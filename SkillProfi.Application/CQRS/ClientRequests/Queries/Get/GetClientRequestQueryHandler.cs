using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.ClientRequests.Queries.Get;

public sealed class GetClientRequestQueryHandler(IAppContext appContext, IMapper mapper) : IRequestHandler<GetClientRequestQuery, ClientRequestDto>
{
	public async Task<ClientRequestDto> Handle(GetClientRequestQuery request, CancellationToken cancellationToken)
	{
		ClientRequest? clientRequest = await appContext.ClientRequests.FirstOrDefaultAsync(clientRequest => clientRequest.Id == request.Id, cancellationToken);

		if (clientRequest == null)
		{
			throw new NotFoundException(nameof(User), request.Id);
		}

		ClientRequestDto clientRequestDto = mapper.Map<ClientRequestDto>(clientRequest);

		return clientRequestDto;
	}
}
