using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Application.CQRS.SiteItems.Commands.Create;
using SkillProfi.Application.CQRS.SiteItems.Commands.Delete;
using SkillProfi.Application.CQRS.SiteItems.Commands.Update;
using SkillProfi.Application.CQRS.SiteItems.Queries.Get;
using SkillProfi.WebApi.Models.SiteItem;

namespace SkillProfi.WebApi.Controllers;

public class SiteItemController(IMapper mapper) : BaseController
{
	[HttpGet("{key}")]
	public async Task<ActionResult<SiteItemDto>> Get(string key, CancellationToken cancellationToken)
	{
		GetSiteItemQuery query = new() { Key = key };
		SiteItemDto siteItemDto = await Mediator.Send(query, cancellationToken);

		return Ok(siteItemDto);
	}
	
	[HttpPost]
	[Authorize]
	public async Task<ActionResult<int>> Create([FromBody] CreateSiteItemModel createSiteItemModel)
	{
		CreateSiteItemCommand command = mapper.Map<CreateSiteItemCommand>(createSiteItemModel);
		command.CreatedBy = UserId;
		int clientRequestId = await Mediator.Send(command);

		return Ok(clientRequestId);
	}
	
	[HttpPut]
	[Authorize]
	public async Task<IActionResult> Update([FromBody] UpdateSiteItemModel updateSiteItemModel, CancellationToken cancellationToken)
	{
		UpdateSiteItemCommand command = mapper.Map<UpdateSiteItemCommand>(updateSiteItemModel);
		command.UpdatedById = UserId;
		await Mediator.Send(command, cancellationToken);

		return NoContent();
	}

	
	[HttpDelete("{key}")]
	[Authorize]
	public async Task<IActionResult> Delete(string key, CancellationToken cancellationToken)
	{
		DeleteSiteItemCommand command = new() { Key = key };
		await Mediator.Send(command, cancellationToken);

		return NoContent();
	}
}
