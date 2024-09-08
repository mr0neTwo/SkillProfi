using MediatR;

namespace SkillProfi.Application.CQRS.Users.Commands.Delete;

public sealed class DeleteUserCommand : IRequest<Unit>
{
	public int Id { get; set; }
}