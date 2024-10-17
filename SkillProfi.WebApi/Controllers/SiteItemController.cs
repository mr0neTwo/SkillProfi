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
    /// <summary>
    /// Retrieves a site item by its key.
    /// </summary>
    /// <param name="key">The unique key of the site item</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The requested site item</returns>
    /// <response code="200">Returns the requested site item</response>
    /// <response code="404">If the site item is not found</response>
    [HttpGet("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SiteItemDto>> Get(string key, CancellationToken cancellationToken)
    {
        GetSiteItemQuery query = new() { Key = key };
        SiteItemDto siteItemDto = await Mediator.Send(query, cancellationToken);

        return Ok(siteItemDto);
    }

    /// <summary>
    /// Retrieves all site items.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A dictionary of all site items</returns>
    /// <response code="200">Returns a dictionary of site items</response>
    /// <response code="401">Unauthorized access</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Dictionary<string, string>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllSiteItemQuery query = new();
        Dictionary<string, string> result = await Mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Creates a new site item.
    /// </summary>
    /// <param name="createSiteItemDto">Data for creating the site item</param>
    /// <returns>The ID of the created site item</returns>
    /// <response code="200">Site item created successfully</response>
    /// <response code="400">If the input data is invalid</response>
    /// <response code="401">Unauthorized access</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<int>> Create([FromBody] CreateSiteItemDto createSiteItemDto)
    {
        CreateSiteItemCommand command = mapper.Map<CreateSiteItemCommand>(createSiteItemDto);
        command.CreatedBy = UserId;
        int clientRequestId = await Mediator.Send(command);

        return Ok(clientRequestId);
    }
    
    /// <summary>
    /// Updates an existing site item.
    /// </summary>
    /// <param name="updateSiteItemDto">Data for updating the site item</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Site item updated successfully</response>
    /// <response code="400">If the input data is invalid</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="404">If the site item is not found</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateSiteItemDto updateSiteItemDto, CancellationToken cancellationToken)
    {
        UpdateSiteItemCommand command = mapper.Map<UpdateSiteItemCommand>(updateSiteItemDto);
        command.UpdatedById = UserId;
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }
    
    /// <summary>
    /// Updates multiple site items.
    /// </summary>
    /// <param name="siteItemDictionary">A dictionary of site items to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Site items updated successfully</response>
    /// <response code="401">Unauthorized access</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
    
    /// <summary>
    /// Deletes a site item by its key.
    /// </summary>
    /// <param name="key">The unique key of the site item</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Site item deleted successfully</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="404">If the site item is not found</response>
    [HttpDelete("{key}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string key, CancellationToken cancellationToken)
    {
        DeleteSiteItemCommand command = new() { Key = key };
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }
}

