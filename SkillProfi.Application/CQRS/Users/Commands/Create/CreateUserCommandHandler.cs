using MediatR;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Users.Commands.Create;

public sealed class CreateUserCommandHandler(IAppContext appContext) : IRequestHandler<CreateUserCommand, int>
{
	public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		User user = new()
		{
			Name = request.Name,
			Email = request.Email,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
			CreationDate = DateTime.Now,
			CreatedById = request.CreatedBy
		};

		await appContext.Users.AddAsync(user, cancellationToken);
		await appContext.SaveChangesAsync(cancellationToken);

		return user.Id;
	}
}
