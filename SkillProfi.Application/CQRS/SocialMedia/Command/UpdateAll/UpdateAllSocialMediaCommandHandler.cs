using MediatR;
using SkillProfi.Application.CQRS.SocialMedia.Queries.GetList;
using SkillProfi.Application.Interfaces;

namespace SkillProfi.Application.CQRS.SocialMedia.Command.UpdateAll;

public sealed class UpdateAllSocialMediaCommandHandler(IAppContext appContext) : IRequestHandler<UpdateAllSocialMediaCommand,
	Unit>
{
	public async Task<Unit> Handle(UpdateAllSocialMediaCommand request, CancellationToken cancellationToken)
	{
		appContext.SocialMedias.RemoveRange(appContext.SocialMedias.ToList());
		await appContext.SaveChangesAsync(cancellationToken);

		List<Domain.SocialMedia> socialMedias = new();

		foreach (SocialMediaDto socialMediaDto in request.SocialMedias)
		{
			Domain.SocialMedia socialMedia = new()
			{
				Link = socialMediaDto.Link,
				IconName = socialMediaDto.IconName,
				CreationDate = DateTime.Now,
				CreatedById = request.UpdatedById
			};
			
			socialMedias.Add(socialMedia);
		}
		
		await appContext.SocialMedias.AddRangeAsync(socialMedias, cancellationToken);
		await appContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
