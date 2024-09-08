using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.SiteItems.Queries.Get;

public sealed class GetSiteItemQueryHandler(IAppContext appContext, IMapper mapper) : IRequestHandler<GetSiteItemQuery, SiteItemDto>
{
	public async Task<SiteItemDto> Handle(GetSiteItemQuery request, CancellationToken cancellationToken)
	{
		SiteItem? siteItem = await appContext.SiteItems.FirstOrDefaultAsync(siteItem => siteItem.Key == request.Key, cancellationToken);

		if (siteItem == null)
		{
			throw new NotFoundException(nameof(User), request.Key);
		}

		SiteItemDto siteItemDto = mapper.Map<SiteItemDto>(siteItem);

		return siteItemDto;
	}
}
