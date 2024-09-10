using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Application.CQRS.Company.Command.Update;
using SkillProfi.Application.CQRS.Company.Queries.Get;
using SkillProfi.WebApi.Models.Company;

namespace SkillProfi.WebApi.Controllers;

public class CompanyController(IMapper mapper) : BaseController
{
	[HttpGet]
	public async Task<ActionResult<CompanyDto>> Get()
	{
		GetCompanyQuery query = new();
		CompanyDto response = await Mediator.Send(query);

		return Ok(response);
	}
	
	[HttpPut]
	[Authorize]
	public async Task<IActionResult> Update([FromBody] UpdateCompanyDto updateCompanyDto)
	{
		UpdateCompanyCommand updateCompanyCommand = mapper.Map<UpdateCompanyCommand>(updateCompanyDto);
		
		updateCompanyCommand.UpdatedById = UserId;
		await Mediator.Send(updateCompanyCommand);

		return NoContent();
	}
}
