using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Users.Queries.Get;

public sealed class GetUserQueryHandler(IAppContext appContext, IMapper mapper) : IRequestHandler<GetUserQuery, UserDto>
{
	public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
	{
		User? user = await appContext.Users.FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);

		if (user == null)
		{
			throw new NotFoundException(nameof(User), request.Id);
		}

		UserDto userDto = mapper.Map<UserDto>(user);

		return userDto;
	}
}
