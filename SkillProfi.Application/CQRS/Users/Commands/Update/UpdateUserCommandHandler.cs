using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Users.Commands.Update;

public sealed class UpdateUserCommandHandler(IAppContext appContext) : IRequestHandler<UpdateUserCommand, Unit>
{
	public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		User? user = await appContext.Users.FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);

		if (user == null)
		{
			throw new NotFoundException(nameof(User), request.Id);
		}

		if (request.Name != null)
		{
			user.Name = request.Name;
		}
		
		if (request.Email != null)
		{
			user.Email = request.Email;
		}
		
		if (request.Password != null)
		{
			user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
		}
		
		user.UpdatingDate = DateTime.Now;
		user.UpdatedById = request.UpdatedBy;
		
		await appContext.SaveChangesAsync(cancellationToken);
		
		return Unit.Value;
	}
}
