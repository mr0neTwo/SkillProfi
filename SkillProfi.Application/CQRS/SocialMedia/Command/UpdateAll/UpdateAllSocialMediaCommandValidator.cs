using FluentValidation;

namespace SkillProfi.Application.CQRS.SocialMedia.Command.UpdateAll;

public sealed class UpdateAllSocialMediaCommandValidator : AbstractValidator<UpdateAllSocialMediaCommand>
{
	public UpdateAllSocialMediaCommandValidator()
	{
		RuleForEach(command => command.SocialMedias)
			.SetValidator(new SocialMediaDtoValidator());
	}
}
