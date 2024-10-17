using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Application.CQRS.Projects.Command.Create;
using SkillProfi.Application.CQRS.Projects.Command.Delete;
using SkillProfi.Application.CQRS.Projects.Command.Update;
using SkillProfi.Application.CQRS.Projects.Queries.GetImageUrl;
using SkillProfi.Application.CQRS.Projects.Queries.GetList;
using SkillProfi.WebApi.Models.Projects;
using SkillProfi.WebApi.Services.ImageService;

namespace SkillProfi.WebApi.Controllers;

public class ProjectController(IMapper mapper, IImageStore imageStore) : BaseController
{
    /// <summary>
    /// Retrieves a list of projects based on the provided query parameters.
    /// </summary>
    /// <param name="query">Query parameters for filtering the project list</param>
    /// <returns>A list of projects</returns>
    /// <response code="200">Returns the list of projects</response>
    /// <response code="401">Unauthorized access</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetProjectListResponse>> GetList([FromQuery] GetProjectListQuery query)
    {
        GetProjectListResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="createProjectDto">Data for creating the project</param>
    /// <returns>The ID of the created project</returns>
    /// <response code="200">Project created successfully</response>
    /// <response code="400">If the input data is invalid</response>
    /// <response code="401">Unauthorized access</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<int>> Create([FromBody] CreateProjectDto createProjectDto)
    {
        CreateProjectCommand command = mapper.Map<CreateProjectCommand>(createProjectDto);
        
        if (!string.IsNullOrEmpty(createProjectDto.ImageBase64))
        {
            command.ImageUrl = await imageStore.SaveImageAsync(createProjectDto.ImageBase64);
        }
        
        command.CreatedBy = UserId;
        int projectId = await Mediator.Send(command);

        return Ok(projectId);
    }

    /// <summary>
    /// Updates an existing project.
    /// </summary>
    /// <param name="updateProjectDto">Data for updating the project</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Project updated successfully</response>
    /// <response code="400">If the input data is invalid</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="404">If the project is not found</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateProjectDto updateProjectDto)
    {
        UpdateProjectCommand updateProjectCommand = mapper.Map<UpdateProjectCommand>(updateProjectDto);

        if (!string.IsNullOrEmpty(updateProjectDto.ImageBase64))
        {
            updateProjectCommand.ImageUrl = await imageStore.SaveImageAsync(updateProjectDto.ImageBase64);
            GetProjectImageUrlQuery projectImageUrlQuery = new() { Id = updateProjectCommand.Id };
            string? oldImageUrl = await Mediator.Send(projectImageUrlQuery);

            if (!string.IsNullOrEmpty(oldImageUrl))
            {
                imageStore.DeleteImage(oldImageUrl);
            }
        }
        
        updateProjectCommand.UpdatedById = UserId;
        await Mediator.Send(updateProjectCommand);

        return NoContent();
    }

    /// <summary>
    /// Deletes a project by ID.
    /// </summary>
    /// <param name="id">The ID of the project to delete</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Project deleted successfully</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="404">If the project is not found</response>
    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        GetProjectImageUrlQuery projectImageUrlQuery = new() { Id = id };
        string? imageUrl = await Mediator.Send(projectImageUrlQuery);

        if (!string.IsNullOrEmpty(imageUrl))
        {
            imageStore.DeleteImage(imageUrl);
        }
        
        DeleteProjectCommand command = new() { Id = id };
        await Mediator.Send(command);

        return NoContent();
    }
}
