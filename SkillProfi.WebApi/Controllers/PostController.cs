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
	[HttpGet]
	public async Task<ActionResult<GetPostListResponse>> GetList([FromQuery] GetPostListQuery query)
	{
		GetPostListResponse response = await Mediator.Send(query);

		return Ok(response);
	}
	
	[HttpPost]
	[Authorize]
	public async Task<ActionResult<int>> Create([FromBody] CreatePostModel createPostModel)
	{
		CreatePostCommand command = mapper.Map<CreatePostCommand>(createPostModel);
		
		if (!string.IsNullOrEmpty(createPostModel.ImageBase64))
		{
			command.ImageUrl = await imageStore.SaveImageAsync(createPostModel.ImageBase64);
		}
		
		command.CreatedBy = UserId;
		int postId = await Mediator.Send(command);

		return Ok(postId);
	}
	
	[HttpPut]
	[Authorize]
	public async Task<IActionResult> Update([FromBody] UpdatePostModel updatePostModel)
	{
		UpdatePostCommand updatePostCommand = mapper.Map<UpdatePostCommand>(updatePostModel);

		if (!string.IsNullOrEmpty(updatePostModel.ImageBase64))
		{
			updatePostCommand.ImageUrl = await imageStore.SaveImageAsync(updatePostModel.ImageBase64);
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

	
	[HttpDelete("{id:int}")]
	[Authorize]
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
