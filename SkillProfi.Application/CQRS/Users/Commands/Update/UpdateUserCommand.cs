using MediatR;

namespace SkillProfi.Application.CQRS.Users.Commands.Update;

public sealed class UpdateUserCommand : IRequest<Unit>
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Email { get; set; }
	public string? Password { get; set; }
	public int UpdatedBy { get; set; }
}