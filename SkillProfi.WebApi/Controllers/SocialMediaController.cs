using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Application.CQRS.SocialMedia.Command.Create;
using SkillProfi.Application.CQRS.SocialMedia.Command.Delete;
using SkillProfi.Application.CQRS.SocialMedia.Command.Update;
using SkillProfi.Application.CQRS.SocialMedia.Command.UpdateAll;
using SkillProfi.Application.CQRS.SocialMedia.Queries.GetList;
using SkillProfi.WebApi.Models.SocialMedia;

namespace SkillProfi.WebApi.Controllers;

public class SocialMediaController(IMapper mapper) : BaseController
{
	[HttpGet]
	public async Task<ActionResult<List<SocialMediaDto>>> GetList()
	{
		GetSocialMediaListQuery query = new();
		List<SocialMediaDto> response = await Mediator.Send(query);

		return Ok(response);
	}
	
	[HttpPost]
	[Authorize]
	public async Task<ActionResult<int>> Create([FromBody] CreateSocialMediaDto createSocialMediaDto)
	{
		CreateSocialMediaCommand command = mapper.Map<CreateSocialMediaCommand>(createSocialMediaDto);
		
		command.CreatedById = UserId;
		int socialMediaId = await Mediator.Send(command);

		return Ok(socialMediaId);
	}
	
	[HttpPut]
	[Authorize]
	public async Task<IActionResult> Update([FromBody] UpdateSocialMediaDto updateSocialMediaDto)
	{
		UpdateSocialMediaCommand command = mapper.Map<UpdateSocialMediaCommand>(updateSocialMediaDto);
		command.UpdatedById = UserId;
		await Mediator.Send(command);

		return NoContent();
	}
	
	[HttpPut]
	[Authorize]
	public async Task<IActionResult> UpdateAll([FromBody] List<SocialMediaDto> socialMediaDtos)
	{
		UpdateAllSocialMediaCommand command = new()
		{
			SocialMedias = socialMediaDtos,
			UpdatedById = UserId
		};
		
		await Mediator.Send(command);

		return NoContent();
	}
	
	[HttpDelete("{id:int}")]
	[Authorize]
	public async Task<IActionResult> Delete(int id)
	{
		DeleteSocialMediaCommand command = new() { Id = id };
		await Mediator.Send(command);

		return NoContent();
	}
}
