using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.CQRS.Users.Queries.Get;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Users.Queries.GetList;

public sealed class GetUserListHandler(IAppContext appContext, IMapper mapper) : IRequestHandler<GetUserListQuery, List<UserDto>>
{
	public async Task<List<UserDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
	{
		List<User> users = await appContext.Users
										   .OrderBy(user => user.Id)
										   .ToListAsync(cancellationToken);

		List<UserDto> userDtos = mapper.Map<List<UserDto>>(users);

		return userDtos;
	}
}
