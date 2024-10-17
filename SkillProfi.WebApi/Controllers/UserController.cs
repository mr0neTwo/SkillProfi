using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Application.CQRS.Users.Commands.Create;
using SkillProfi.Application.CQRS.Users.Commands.Delete;
using SkillProfi.Application.CQRS.Users.Commands.Update;
using SkillProfi.Application.CQRS.Users.Queries.Get;
using SkillProfi.Application.CQRS.Users.Queries.GetList;
using SkillProfi.WebApi.Models.Users;

namespace SkillProfi.WebApi.Controllers;

public class UserController(IMapper mapper) : BaseController
{
    /// <summary>
    /// Retrieves a list of users.
    /// </summary>
    /// <returns>A list of user entries</returns>
    /// <response code="200">Returns a list of users</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<UserDto>>> GetList()
    {
        GetUserListQuery query = new();
        List<UserDto> userDtos = await Mediator.Send(query);

        return Ok(userDtos);
    }
    
    /// <summary>
    /// Retrieves a specific user by ID.
    /// </summary>
    /// <param name="id">The unique ID of the user</param>
    /// <returns>A user entry</returns>
    /// <response code="200">Returns the user</response>
    /// <response code="404">If the user is not found</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get(int id)
    {
        GetUserQuery query = new() { Id = id };
        UserDto userDto = await Mediator.Send(query);

        return Ok(userDto);
    }
    
    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="createUserDto">Data for creating the user</param>
    /// <returns>The ID of the created user</returns>
    /// <response code="200">User created successfully</response>
    /// <response code="400">If the input data is invalid</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create([FromBody] CreateUserDto createUserDto)
    {
        CreateUserCommand command = mapper.Map<CreateUserCommand>(createUserDto);
        command.CreatedBy = UserId;
        int userId = await Mediator.Send(command);

        return Ok(userId);
    }
    
    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="updateUserDto">Data for updating the user</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">User updated successfully</response>
    /// <response code="400">If the input data is invalid</response>
    /// <response code="404">If the user is not found</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
    {
        UpdateUserCommand command = mapper.Map<UpdateUserCommand>(updateUserDto);
        command.UpdatedBy = UserId;
        await Mediator.Send(command);

        return NoContent();
    }
    
    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="id">The unique ID of the user</param>
    /// <returns>HTTP 204 (No Content)</returns>
    /// <response code="204">User deleted successfully</response>
    /// <response code="404">If the user is not found</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        DeleteUserCommand command = new() { Id = id };
        await Mediator.Send(command);

        return NoContent();
    }
}

