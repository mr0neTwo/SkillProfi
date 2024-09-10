using MediatR;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;

namespace SkillProfi.Application.CQRS.SocialMedia.Command.Delete;

public sealed class DeleteSocialMediaCommandHandler(IAppContext appContext) : IRequestHandler<DeleteSocialMediaCommand, Unit>
{
	public async Task<Unit> Handle(DeleteSocialMediaCommand request, CancellationToken cancellationToken)
	{
		Domain.SocialMedia? socialMedia = await appContext.SocialMedias.FindAsync([request.Id], cancellationToken);

		if (socialMedia == null)
		{
			throw new NotFoundException(nameof(Domain.SocialMedia), request.Id);
		}

		appContext.SocialMedias.Remove(socialMedia);
		await appContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
