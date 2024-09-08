using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.SiteItems.Commands.Delete;

public sealed class DeleteSiteItemCommandHandler(IAppContext appContext) : IRequestHandler<DeleteSiteItemCommand, Unit>
{
	public async Task<Unit> Handle(DeleteSiteItemCommand request, CancellationToken cancellationToken)
	{
		SiteItem? siteItem = await appContext.SiteItems.FirstOrDefaultAsync(siteItem => siteItem.Key == request.Key, cancellationToken);

		if (siteItem == null)
		{
			throw new NotFoundException(nameof(ClientRequest), request.Key);
		}

		appContext.SiteItems.Remove(siteItem);
		await appContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
