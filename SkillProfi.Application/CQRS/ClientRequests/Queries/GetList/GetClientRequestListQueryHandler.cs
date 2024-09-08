using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.CQRS.ClientRequests.Queries.Get;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.ClientRequests.Queries.GetList;

public sealed class GetClientRequestListQueryHandler(IAppContext appContext, IMapper mapper)
	: IRequestHandler<GetClientRequestListQuery, GetClientRequestResponse>
{
	public async Task<GetClientRequestResponse> Handle(GetClientRequestListQuery request, CancellationToken cancellationToken)
	{
		DateTime startDate = DateTimeOffset.FromUnixTimeSeconds(request.StartTimestamp).DateTime;
		DateTime endDate = DateTimeOffset.FromUnixTimeSeconds(request.EndTimeStamp).DateTime;

		IOrderedQueryable<ClientRequest> query = appContext.ClientRequests
														   .Where(clientRequest => clientRequest.CreationDate >= startDate && clientRequest.CreationDate <= endDate)
														   .OrderBy(clientRequest => clientRequest.Id);
		
		int count = await query.CountAsync(cancellationToken);

		int skip = (request.PageNumber - 1) * request.PageSize;

		List<ClientRequest> clientRequests = await query
												   .Skip(skip)
												   .Take(request.PageSize)
												   .ToListAsync(cancellationToken);
		
		List<ClientRequestDto> clientRequestDtos = mapper.Map<List<ClientRequestDto>>(clientRequests);

		return new GetClientRequestResponse()
		{
			ClientRequests = clientRequestDtos,
			Count = count,
			PageNumber = request.PageNumber,
			TotalPages = (int)Math.Ceiling((double)count / request.PageSize)
		};
	}
}