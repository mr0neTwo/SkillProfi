﻿using MediatR;

namespace SkillProfi.Application.CQRS.Users.Commands.Create;

public sealed class CreateUserCommand : IRequest<int>
{
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public int CreatedBy { get; set; }
}