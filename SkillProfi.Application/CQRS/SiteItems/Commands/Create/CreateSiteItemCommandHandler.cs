using MediatR;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.SiteItems.Commands.Create;

public class CreateSiteItemCommandHandler(IAppContext appContext) : IRequestHandler<CreateSiteItemCommand, int>
{
	public async Task<int> Handle(CreateSiteItemCommand request, CancellationToken cancellationToken)
	{
		SiteItem siteItem = new()
		{
			CreationDate = DateTime.Now,
			CreatedById = request.CreatedBy,
			Key = request.Key,
			Title = request.Title
		};
		
		await appContext.SiteItems.AddAsync(siteItem, cancellationToken);
		await appContext.SaveChangesAsync(cancellationToken);

		return siteItem.Id;
	}
}
