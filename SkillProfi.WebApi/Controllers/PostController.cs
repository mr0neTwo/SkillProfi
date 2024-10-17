using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Application.CQRS.Posts.Command.Create;
using SkillProfi.Application.CQRS.Posts.Command.Delete;
using SkillProfi.Application.CQRS.Posts.Command.Update;
using SkillProfi.Application.CQRS.Posts.Queries.GetImageUrl;
using SkillProfi.Application.CQRS.Posts.Queries.GetList;
using SkillProfi.WebApi.Models.Posts;
using SkillProfi.WebApi.Services.ImageService;

namespace SkillProfi.WebApi.Controllers;

public class PostController(IMapper mapper, IImageStore imageStore) : BaseController
{
    /// <summary>
    /// Retrieves a list of posts based on the provided query parameters.
    /// </summary>
    /// <param name="query">Query parameters for filtering the post list</param>
    /// <returns>A list of posts</returns>
    /// <response code="200">Returns the list of posts</response>
    /// <response code="400">If the input data is invalid</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetPostListResponse>> GetList([FromQuery] GetPostListQuery query)
    {
        GetPostListResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    /// <summary>
    /// Creates a new post.
    /// </summary>
    /// <param name="createPostDto">Data for creating the post</param>
    /// <returns>The ID of the created post</returns>
    /// <response code="200">Post created successfully</response>
    /// <response code="400">If the input data is invalid</response>
    /// <response code="401">Unauthorized access</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<int>> Create([FromBody] CreatePostDto createPostDto)
    {
        CreatePostCommand command = mapper.Map<CreatePostCommand>(createPostDto);
        
        if (!string.IsNullOrEmpty(createPostDto.ImageBase64))
        {
            command.ImageUrl = await imageStore.SaveImageAsync(createPostDto.ImageBase64);
        }
        
        command.CreatedBy = UserId;
        int postId = await Mediator.Send(command);

        return Ok(postId);
    }

    /// <summary>
    /// Updates an existing post.
    /// </summary>
    /// <param name="updatePostDto">Data for updating the post</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Post updated successfully</response>
    /// <response code="400">If the input data is invalid</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="404">If the post is not found</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdatePostDto updatePostDto)
    {
        UpdatePostCommand updatePostCommand = mapper.Map<UpdatePostCommand>(updatePostDto);

        if (!string.IsNullOrEmpty(updatePostDto.ImageBase64))
        {
            updatePostCommand.ImageUrl = await imageStore.SaveImageAsync(updatePostDto.ImageBase64);
            GetPostImageUrlQuery postImageUrlQuery = new() { Id = updatePostCommand.Id };
            string? oldImageUrl = await Mediator.Send(postImageUrlQuery);

            if (!string.IsNullOrEmpty(oldImageUrl))
            {
                imageStore.DeleteImage(oldImageUrl);
            }
        }
        
        updatePostCommand.UpdatedById = UserId;
        await Mediator.Send(updatePostCommand);

        return NoContent();
    }

    /// <summary>
    /// Deletes a post by ID.
    /// </summary>
    /// <param name="id">The ID of the post to delete</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Post deleted successfully</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="404">If the post is not found</response>
    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        GetPostImageUrlQuery postImageUrlQuery = new() { Id = id };
        string? imageUrl = await Mediator.Send(postImageUrlQuery);

        if (!string.IsNullOrEmpty(imageUrl))
        {
            imageStore.DeleteImage(imageUrl);
        }
        
        DeletePostCommand command = new() { Id = id };
        await Mediator.Send(command);

        return NoContent();
    }
}

