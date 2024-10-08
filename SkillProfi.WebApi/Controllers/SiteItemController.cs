using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Application.CQRS.SiteItems.Commands.Create;
using SkillProfi.Application.CQRS.SiteItems.Commands.Delete;
using SkillProfi.Application.CQRS.SiteItems.Commands.Update;
using SkillProfi.Application.CQRS.SiteItems.Commands.UpdateAll;
using SkillProfi.Application.CQRS.SiteItems.Queries.Get;
using SkillProfi.Application.CQRS.SiteItems.Queries.GetAll;
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

	[HttpGet]
	[Authorize]
	public async Task<ActionResult<Dictionary<string, string>>> GetAll(CancellationToken cancellationToken)
	{
		GetAllSiteItemQuery query = new();
		Dictionary<string, string> result = await Mediator.Send(query, cancellationToken);
		
		return Ok(result);
	}
	
	[HttpPost]
	[Authorize]
	public async Task<ActionResult<int>> Create([FromBody] CreateSiteItemDto createSiteItemDto)
	{
		CreateSiteItemCommand command = mapper.Map<CreateSiteItemCommand>(createSiteItemDto);
		command.CreatedBy = UserId;
		int clientRequestId = await Mediator.Send(command);

		return Ok(clientRequestId);
	}
	
	[HttpPut]
	[Authorize]
	public async Task<IActionResult> Update([FromBody] UpdateSiteItemDto updateSiteItemDto, CancellationToken cancellationToken)
	{
		UpdateSiteItemCommand command = mapper.Map<UpdateSiteItemCommand>(updateSiteItemDto);
		command.UpdatedById = UserId;
		await Mediator.Send(command, cancellationToken);

		return NoContent();
	}
	
	[HttpPut]
	[Authorize]
	public async Task<IActionResult> UpdateAll([FromBody] Dictionary<string, string> siteItemDictionary, CancellationToken cancellationToken)
	{
		UpdateAllSiteItemCommand command = new()
		{
			SiteItemDictionary = siteItemDictionary, 
			UpdatedById = UserId
		};
		
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
