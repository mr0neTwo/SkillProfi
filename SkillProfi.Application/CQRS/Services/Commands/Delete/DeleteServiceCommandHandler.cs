using MediatR;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Services.Commands.Delete;

public sealed class DeleteServiceCommandHandler(IAppContext appContext) : IRequestHandler<DeleteServiceCommand, Unit>
{
	public async Task<Unit> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
	{
		Service? service = await appContext.Services.FindAsync([request.Id], cancellationToken);

		if (service == null)
		{
			throw new NotFoundException(nameof(Service), request.Id);
		}

		appContext.Services.Remove(service);
		await appContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
