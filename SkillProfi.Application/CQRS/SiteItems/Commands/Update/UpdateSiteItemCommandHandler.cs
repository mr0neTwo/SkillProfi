using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.SiteItems.Commands.Update;

public class UpdateSiteItemCommandHandler(IAppContext appContext) : IRequestHandler<UpdateSiteItemCommand, Unit>
{
	public async Task<Unit> Handle(UpdateSiteItemCommand request, CancellationToken cancellationToken)
	{
		SiteItem? siteItem = await appContext.SiteItems.FirstOrDefaultAsync(siteItem => siteItem.Key == request.Key, cancellationToken);
		
		if (siteItem == null)
		{
			throw new NotFoundException(nameof(SiteItem), request.Key);
		}
		
		siteItem.Title = request.Title;
		siteItem.UpdatingDate = DateTime.Now;
		siteItem.UpdatedById = request.UpdatedById;
		
		appContext.SiteItems.Update(siteItem);
		await appContext.SaveChangesAsync(cancellationToken);
		
		return Unit.Value;
	}
}
