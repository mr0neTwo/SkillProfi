using MediatR;

namespace SkillProfi.Application.CQRS.Users.Queries.Get;

public sealed class GetUserQuery : IRequest<UserDto>
{
	public int Id { get; set; }
}