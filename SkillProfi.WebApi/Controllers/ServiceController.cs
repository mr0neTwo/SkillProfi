using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Application.CQRS.Services.Commands.Create;
using SkillProfi.Application.CQRS.Services.Commands.Delete;
using SkillProfi.Application.CQRS.Services.Commands.Update;
using SkillProfi.Application.CQRS.Services.Queries.GetList;
using SkillProfi.WebApi.Models.Services;

namespace SkillProfi.WebApi.Controllers;

public class ServiceController(IMapper mapper) : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetServiceListResponse>> GetList([FromQuery] GetServiceListQuery query)
	{
		GetServiceListResponse response = await Mediator.Send(query);

		return Ok(response);
	}
	
	[HttpPost]
	[Authorize]
	public async Task<ActionResult<int>> Create([FromBody] CreateServiceModel createServiceModel)
	{
		CreateServiceCommand command = mapper.Map<CreateServiceCommand>(createServiceModel);
		command.CreatedBy = UserId;
		int serviceId = await Mediator.Send(command);

		return Ok(serviceId);
	}
	
	[HttpPut]
	[Authorize]
	public async Task<IActionResult> Update([FromBody] UpdateServiceModel updateServiceModel)
	{
		UpdateServiceCommand command = mapper.Map<UpdateServiceCommand>(updateServiceModel);
		command.UpdatedBy = UserId;
		await Mediator.Send(command);

		return NoContent();
	}

	
	[HttpDelete("{id:int}")]
	[Authorize]
	public async Task<IActionResult> Delete(int id)
	{
		DeleteServiceCommand command = new() { Id = id };
		await Mediator.Send(command);

		return NoContent();
	}
}
