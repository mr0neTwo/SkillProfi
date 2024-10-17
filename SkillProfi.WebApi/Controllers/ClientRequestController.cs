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
    /// <summary>
    /// Retrieves a list of client requests.
    /// </summary>
    /// <param name="query">Query parameters for filtering client requests</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of client requests</returns>
    /// <response code="200">Returns the list of client requests</response>
    /// <response code="401">Unauthorized access</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetClientRequestResponse>> GetList([FromQuery] GetClientRequestListQuery query, CancellationToken cancellationToken)
    {
        GetClientRequestResponse response = await Mediator.Send(query, cancellationToken);

        return Ok(response);
    }
    
    /// <summary>
    /// Retrieves a specific client request by ID.
    /// </summary>
    /// <param name="id">Client request ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The details of the client request</returns>
    /// <response code="200">Returns the client request details</response>
    /// <response code="404">If the client request is not found</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="400">If the input data is invalid</response>
    [HttpGet("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientRequestDto>> Get(int id, CancellationToken cancellationToken)
    {
        GetClientRequestQuery query = new() { Id = id };
        ClientRequestDto clientRequestDto = await Mediator.Send(query, cancellationToken);

        return Ok(clientRequestDto);
    }
    
    /// <summary>
    /// Creates a new client request.
    /// </summary>
    /// <param name="createClientRequestDto">Data for creating a new client request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The ID of the created client request</returns>
    /// <response code="200">Client request created successfully</response>
    /// <response code="400">If the input data is invalid</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create([FromBody] CreateClientRequestDto createClientRequestDto, CancellationToken cancellationToken)
    {
        CreateClientRequestCommand command = mapper.Map<CreateClientRequestCommand>(createClientRequestDto);
        int clientRequestId = await Mediator.Send(command, cancellationToken);

        return Ok(clientRequestId);
    }
    
    /// <summary>
    /// Updates an existing client request.
    /// </summary>
    /// <param name="updateClientRequestDto">Data for updating the client request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Client request updated successfully</response>
    /// <response code="400">If the input data is invalid</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="404">If the client request is not found</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateClientRequestDto updateClientRequestDto, CancellationToken cancellationToken)
    {
        UpdateClientRequestCommand command = mapper.Map<UpdateClientRequestCommand>(updateClientRequestDto);
        command.UpdatedBy = UserId;
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Deletes a client request by ID.
    /// </summary>
    /// <param name="id">Client request ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Client request deleted successfully</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="404">If the client request is not found</response>
    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        DeleteClientRequestCommand command = new() { Id = id };
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }
}


