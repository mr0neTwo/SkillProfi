using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.SiteItems.Queries.GetAll;

public sealed class GetAllSiteItemQueryHandler(IAppContext appContext, IMapper mapper) : IRequestHandler<GetAllSiteItemQuery, Dictionary<string, string>>
{
	public async Task<Dictionary<string, string>> Handle(GetAllSiteItemQuery request, CancellationToken cancellationToken)
	{
		List<SiteItem> siteItemsList = await appContext.SiteItems.ToListAsync(cancellationToken);
		
		Dictionary<string, string> result = new();

		foreach (SiteItem siteItem in siteItemsList)
		{
			result.Add(siteItem.Key, siteItem.Title);
		}
		
		return result;
	}
}
