using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.SiteItems.Commands.UpdateAll;

public class UpdateAllSiteItemCommandHandler(IAppContext appContext) : IRequestHandler<UpdateAllSiteItemCommand, Unit>
{
	public async Task<Unit> Handle(UpdateAllSiteItemCommand request, CancellationToken cancellationToken)
	{
		foreach (string key in request.SiteItemDictionary.Keys)
		{
			SiteItem? siteItem = await appContext.SiteItems.FirstOrDefaultAsync(siteItem => siteItem.Key == key, cancellationToken);
		
			if (siteItem == null)
			{
				throw new NotFoundException(nameof(SiteItem), key);
			}
		
			siteItem.Title = request.SiteItemDictionary[key];
			siteItem.UpdatingDate = DateTime.Now;
			siteItem.UpdatedById = request.UpdatedById;
		
			appContext.SiteItems.Update(siteItem);
		}
		
		await appContext.SaveChangesAsync(cancellationToken);
		
		return Unit.Value;
	}
}
