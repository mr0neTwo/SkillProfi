using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Application.CQRS.ClientRequests.Command.Create;
using SkillProfi.Application.CQRS.ClientRequests.Command.Delete;
using SkillProfi.Application.CQRS.ClientRequests.Command.Update;
using SkillProfi.Application.CQRS.ClientRequests.Queries.Get;
using SkillProfi.Application.CQRS.ClientRequests.Queries.GetList;
using SkillProfi.Application.CQRS.Users.Queries.Get;
using SkillProfi.WebApi.Models.ClientRequests;

namespace SkillProfi.WebApi.Controllers;

public class ClientRequestController(IMapper mapper) : BaseController
{
	[HttpGet]
	[Authorize]
	public async Task<ActionResult<GetClientRequestResponse>> GetList([FromQuery] GetClientRequestListQuery query, CancellationToken cancellationToken)
	{
		GetClientRequestResponse response = await Mediator.Send(query, cancellationToken);

		return Ok(response);
	}
	
	[HttpGet("{id:int}")]
	[Authorize]
	public async Task<ActionResult<UserDto>> Get(int id, CancellationToken cancellationToken)
	{
		GetClientRequestQuery query = new() { Id = id };
		ClientRequestDto clientRequestDto = await Mediator.Send(query, cancellationToken);

		return Ok(clientRequestDto);
	}
	
	[HttpPost]
	public async Task<ActionResult<int>> Create([FromBody] CreateClientRequestModel createClientRequestModel, CancellationToken cancellationToken)
	{
		CreateClientRequestCommand command = mapper.Map<CreateClientRequestCommand>(createClientRequestModel);
		int clientRequestId = await Mediator.Send(command, cancellationToken);

		return Ok(clientRequestId);
	}
	
	[HttpPut]
	[Authorize]
	public async Task<IActionResult> Update([FromBody] UpdateClientRequestModel updateClientRequestModel, CancellationToken cancellationToken)
	{
		UpdateClientRequestCommand command = mapper.Map<UpdateClientRequestCommand>(updateClientRequestModel);
		command.UpdatedBy = UserId;
		await Mediator.Send(command, cancellationToken);

		return NoContent();
	}

	
	[HttpDelete("{id:int}")]
	[Authorize]
	public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
	{
		DeleteClientRequestCommand command = new() { Id = id };
		await Mediator.Send(command, cancellationToken);

		return NoContent();
	}
}
