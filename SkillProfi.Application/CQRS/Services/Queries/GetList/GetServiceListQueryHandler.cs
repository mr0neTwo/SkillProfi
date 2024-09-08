using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Services.Queries.GetList;

public sealed class GetServiceListQueryHandler(IAppContext appContext, IMapper mapper) : IRequestHandler<GetServiceListQuery, GetServiceListResponse>
{
	public async Task<GetServiceListResponse> Handle(GetServiceListQuery request, CancellationToken cancellationToken)
	{
		int count = await appContext.Services.CountAsync(cancellationToken);
		int skip = (request.PageNumber - 1) * request.PageSize;

		List<Service> services = await appContext.Services
												 .OrderBy(service => service.Id)
												 .Skip(skip)
												 .Take(request.PageSize)
												 .ToListAsync(cancellationToken);

		List<ServiceDto> serviceDtos = mapper.Map<List<ServiceDto>>(services);

		return new GetServiceListResponse
		{
			Services = serviceDtos,
			PageNumber = request.PageNumber,
			Count = count,
			TotalPages = (int)Math.Ceiling((double)count / request.PageSize)
		};
	}
}