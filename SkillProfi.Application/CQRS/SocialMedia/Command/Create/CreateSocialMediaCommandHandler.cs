using MediatR;
using SkillProfi.Application.Interfaces;

namespace SkillProfi.Application.CQRS.SocialMedia.Command.Create;

public sealed class CreateSocialMediaCommandHandler(IAppContext appContext) : IRequestHandler<CreateSocialMediaCommand, int>
{
	public async Task<int> Handle(CreateSocialMediaCommand request, CancellationToken cancellationToken)
	{
		Domain.SocialMedia socialMedia = new()
		{
			CreationDate = DateTime.Now,
			CreatedById = request.CreatedById,
			IconName = request.IconName,
			Link = request.Link,
		};

		await appContext.SocialMedias.AddAsync(socialMedia, cancellationToken);
		await appContext.SaveChangesAsync(cancellationToken);

		return socialMedia.Id;
	}
}
