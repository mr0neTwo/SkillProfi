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
	[HttpGet]
	public async Task<ActionResult<GetProjectListResponse>> GetList([FromQuery] GetProjectListQuery query)
	{
		GetProjectListResponse response = await Mediator.Send(query);

		return Ok(response);
	}
	
	[HttpPost]
	[Authorize]
	public async Task<ActionResult<int>> Create([FromBody] CreateProjectModel createProjectModel)
	{
		CreateProjectCommand command = mapper.Map<CreateProjectCommand>(createProjectModel);
		
		if (!string.IsNullOrEmpty(createProjectModel.ImageBase64))
		{
			command.ImageUrl = await imageStore.SaveImageAsync(createProjectModel.ImageBase64);
		}
		
		command.CreatedBy = UserId;
		int projectId = await Mediator.Send(command);

		return Ok(projectId);
	}
	
	[HttpPut]
	[Authorize]
	public async Task<IActionResult> Update([FromBody] UpdateProjectModel updateProjectModel)
	{
		UpdateProjectCommand updateProjectCommand = mapper.Map<UpdateProjectCommand>(updateProjectModel);

		if (!string.IsNullOrEmpty(updateProjectModel.ImageBase64))
		{
			updateProjectCommand.ImageUrl = await imageStore.SaveImageAsync(updateProjectModel.ImageBase64);
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

	
	[HttpDelete("{id:int}")]
	[Authorize]
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