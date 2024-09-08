using MediatR;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.ClientRequests.Command.Delete;

public sealed class DeleteClientRequestCommandHandler(IAppContext appContext) : IRequestHandler<DeleteClientRequestCommand, Unit>
{
	public async Task<Unit> Handle(DeleteClientRequestCommand request, CancellationToken cancellationToken)
	{
		ClientRequest? clientRequest = await appContext.ClientRequests.FindAsync([request.Id], cancellationToken);

		if (clientRequest == null)
		{
			throw new NotFoundException(nameof(ClientRequest), request.Id);
		}

		appContext.ClientRequests.Remove(clientRequest);
		await appContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
