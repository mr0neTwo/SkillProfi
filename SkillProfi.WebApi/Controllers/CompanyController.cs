using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Application.CQRS.Company.Command.Update;
using SkillProfi.Application.CQRS.Company.Queries.Get;
using SkillProfi.WebApi.Models.Company;

namespace SkillProfi.WebApi.Controllers;

public class CompanyController(IMapper mapper) : BaseController
{
	/// <summary>
	/// Retrieves company details.
	/// </summary>
	/// <returns>Company information</returns>
	/// <response code="200">Returns the company details</response>
	/// <response code="404">If the company information is not found</response>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<CompanyDto>> Get()
	{
		GetCompanyQuery query = new();
		CompanyDto response = await Mediator.Send(query);

		return Ok(response);
	}

	/// <summary>
	/// Updates company details.
	/// </summary>
	/// <param name="updateCompanyDto">Data for updating the company</param>
	/// <returns>HTTP 204 (No Content)</returns>
	/// <response code="204">Company updated successfully</response>
	/// <response code="400">If the input data is invalid</response>
	/// <response code="401">Unauthorized access</response>
	[HttpPut]
	[Authorize]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Update([FromBody] UpdateCompanyDto updateCompanyDto)
	{
		UpdateCompanyCommand updateCompanyCommand = mapper.Map<UpdateCompanyCommand>(updateCompanyDto);
        
		updateCompanyCommand.UpdatedById = UserId;
		await Mediator.Send(updateCompanyCommand);

		return NoContent();
	}
}

