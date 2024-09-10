using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;

namespace SkillProfi.Application.CQRS.SocialMedia.Command.Update;

public sealed class UpdateSocialMediaCommandHandler(IAppContext appContext) : IRequestHandler<UpdateSocialMediaCommand, Unit>
{
	public async Task<Unit> Handle(UpdateSocialMediaCommand request, CancellationToken cancellationToken)
	{
		Domain.SocialMedia? socialMedia = await appContext.SocialMedias.FirstOrDefaultAsync(socialMedia => socialMedia.Id == request.Id, cancellationToken);

		if (socialMedia == null)
		{
			throw new NotFoundException(nameof(SocialMedia), request.Id);
		}

		if (request.IconName != null)
		{
			socialMedia.IconName = request.IconName;
		}
		
		if (request.Link != null)
		{
			socialMedia.Link = request.Link;
		}

		socialMedia.UpdatingDate = DateTime.Now;
		socialMedia.UpdatedById = request.UpdatedById;

		appContext.SocialMedias.Update(socialMedia);
		await appContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
