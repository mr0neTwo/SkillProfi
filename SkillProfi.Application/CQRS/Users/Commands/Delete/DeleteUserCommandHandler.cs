using MediatR;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Users.Commands.Delete;

public sealed class DeleteUserCommandHandler(IAppContext appContext) : IRequestHandler<DeleteUserCommand, Unit>
{
	public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
	{
		User? user = await appContext.Users.FindAsync([request.Id], cancellationToken);

		if (user == null)
		{
			throw new NotFoundException(nameof(User), request.Id);
		}

		appContext.Users.Remove(user);
		await appContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
