using MediatR;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.ClientRequests.Command.Create;

public sealed class CreateClientRequestCommandHandler(IAppContext appContext) : IRequestHandler<CreateClientRequestCommand, int>
{
	public async Task<int> Handle(CreateClientRequestCommand request, CancellationToken cancellationToken)
	{
		ClientRequest clientRequest = new()
		{
			ClientName = request.ClientName,
			ClientEmail = request.ClientEmail,
			Message = request.Message,
			Status = ClientRequestStatus.Received,
			CreationDate = DateTime.Now
		};

		await appContext.ClientRequests.AddAsync(clientRequest, cancellationToken);
		await appContext.SaveChangesAsync(cancellationToken);

		return clientRequest.Id;
	}
}
