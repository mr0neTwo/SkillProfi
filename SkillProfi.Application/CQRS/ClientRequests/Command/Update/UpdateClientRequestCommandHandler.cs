using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.ClientRequests.Command.Update;

public sealed class UpdateClientRequestCommandHandler(IAppContext appContext) : IRequestHandler<UpdateClientRequestCommand, Unit>
{
	public async Task<Unit> Handle(UpdateClientRequestCommand request, CancellationToken cancellationToken)
	{
		ClientRequest? clientRequest = await appContext.ClientRequests.FirstOrDefaultAsync(clientRequest => clientRequest.Id == request.Id, cancellationToken);

		if (clientRequest == null)
		{
			throw new NotFoundException(nameof(ClientRequest), request.Id);
		}
		
		clientRequest.Status = request.Status;
		clientRequest.UpdatingDate = DateTime.Now;
		clientRequest.UpdatedById = request.UpdatedBy;
		await appContext.SaveChangesAsync(cancellationToken);
		
		return Unit.Value;
	}
}
