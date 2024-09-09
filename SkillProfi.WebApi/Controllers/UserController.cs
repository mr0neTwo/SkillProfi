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
	[HttpGet]
	public async Task<ActionResult<List<UserDto>>> GetList()
	{
		GetUserListQuery query = new();
		List<UserDto> userDtos = await Mediator.Send(query);

		return Ok(userDtos);
	}
	
	[HttpGet("{id:int}")]
	public async Task<ActionResult<UserDto>> Get(int id)
	{
		GetUserQuery query = new() { Id = id };
		UserDto userDto = await Mediator.Send(query);

		return Ok(userDto);
	}
	
	[HttpPost]
	public async Task<ActionResult<int>> Create([FromBody] CreateUserDto createUserDto)
	{
		CreateUserCommand command = mapper.Map<CreateUserCommand>(createUserDto);
		command.CreatedBy = UserId;
		int userId = await Mediator.Send(command);

		return Ok(userId);
	}
	
	[HttpPut]
	public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
	{
		UpdateUserCommand command = mapper.Map<UpdateUserCommand>(updateUserDto);
		command.UpdatedBy = UserId;
		await Mediator.Send(command);

		return NoContent();
	}

	
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		DeleteUserCommand command = new() { Id = id};
		await Mediator.Send(command);

		return NoContent();
	}
}
