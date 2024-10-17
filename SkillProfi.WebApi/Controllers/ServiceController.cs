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
    /// <summary>
    /// Retrieves a list of services based on the provided query parameters.
    /// </summary>
    /// <param name="query">Query parameters for filtering the service list</param>
    /// <returns>A list of services</returns>
    /// <response code="200">Returns the list of services</response>
    /// <response code="401">Unauthorized access</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetServiceListResponse>> GetList([FromQuery] GetServiceListQuery query)
    {
        GetServiceListResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    /// <summary>
    /// Creates a new service.
    /// </summary>
    /// <param name="createServiceDto">Data for creating the service</param>
    /// <returns>The ID of the created service</returns>
    /// <response code="200">Service created successfully</response>
    /// <response code="400">If the input data is invalid</response>
    /// <response code="401">Unauthorized access</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<int>> Create([FromBody] CreateServiceDto createServiceDto)
    {
        CreateServiceCommand command = mapper.Map<CreateServiceCommand>(createServiceDto);
        command.CreatedBy = UserId;
        int serviceId = await Mediator.Send(command);

        return Ok(serviceId);
    }

    /// <summary>
    /// Updates an existing service.
    /// </summary>
    /// <param name="updateServiceDto">Data for updating the service</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Service updated successfully</response>
    /// <response code="400">If the input data is invalid</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="404">If the service is not found</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateServiceDto updateServiceDto)
    {
        UpdateServiceCommand command = mapper.Map<UpdateServiceCommand>(updateServiceDto);
        command.UpdatedBy = UserId;
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Deletes a service by ID.
    /// </summary>
    /// <param name="id">The ID of the service to delete</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Service deleted successfully</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="404">If the service is not found</response>
    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        DeleteServiceCommand command = new() { Id = id };
        await Mediator.Send(command);

        return NoContent();
    }
}

