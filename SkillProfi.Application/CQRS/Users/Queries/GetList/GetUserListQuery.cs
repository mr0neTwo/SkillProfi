using MediatR;
using SkillProfi.Application.CQRS.Users.Queries.Get;

namespace SkillProfi.Application.CQRS.Users.Queries.GetList;

public sealed class GetUserListQuery : IRequest<List<UserDto>>
{
}