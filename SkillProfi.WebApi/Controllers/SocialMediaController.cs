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
    /// <summary>
    /// Retrieves a list of social media entries.
    /// </summary>
    /// <returns>A list of social media entries</returns>
    /// <response code="200">Returns a list of social media entries</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SocialMediaDto>>> GetList()
    {
        GetSocialMediaListQuery query = new();
        List<SocialMediaDto> response = await Mediator.Send(query);

        return Ok(response);
    }
    
    /// <summary>
    /// Creates a new social media entry.
    /// </summary>
    /// <param name="createSocialMediaDto">Data for creating the social media entry</param>
    /// <returns>The ID of the created social media entry</returns>
    /// <response code="200">Social media entry created successfully</response>
    /// <response code="400">If the input data is invalid</response>
    /// <response code="401">Unauthorized access</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<int>> Create([FromBody] CreateSocialMediaDto createSocialMediaDto)
    {
        CreateSocialMediaCommand command = mapper.Map<CreateSocialMediaCommand>(createSocialMediaDto);
        command.CreatedById = UserId;
        int socialMediaId = await Mediator.Send(command);

        return Ok(socialMediaId);
    }
    
    /// <summary>
    /// Updates an existing social media entry.
    /// </summary>
    /// <param name="updateSocialMediaDto">Data for updating the social media entry</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Social media entry updated successfully</response>
    /// <response code="400">If the input data is invalid</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="404">If the social media entry is not found</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateSocialMediaDto updateSocialMediaDto)
    {
        UpdateSocialMediaCommand command = mapper.Map<UpdateSocialMediaCommand>(updateSocialMediaDto);
        command.UpdatedById = UserId;
        await Mediator.Send(command);

        return NoContent();
    }
    
    /// <summary>
    /// Updates multiple social media entries.
    /// </summary>
    /// <param name="socialMediaDtos">A list of social media entries to update</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Social media entries updated successfully</response>
    /// <response code="401">Unauthorized access</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
    
    /// <summary>
    /// Deletes a social media entry by its ID.
    /// </summary>
    /// <param name="id">The unique ID of the social media entry</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">Social media entry deleted successfully</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="404">If the social media entry is not found</response>
    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        DeleteSocialMediaCommand command = new() { Id = id };
        await Mediator.Send(command);

        return NoContent();
    }
}

