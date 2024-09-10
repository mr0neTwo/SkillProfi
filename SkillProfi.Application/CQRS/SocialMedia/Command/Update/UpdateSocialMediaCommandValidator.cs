using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.SocialMedia.Command.Update;

public sealed class UpdateSocialMediaCommandValidator : AbstractValidator<UpdateSocialMediaCommand>
{
	public UpdateSocialMediaCommandValidator()
	{
		RuleFor(socialMediaCommand => socialMediaCommand.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");

		When
		(
			updateSocialMediaCommand => !string.IsNullOrEmpty(updateSocialMediaCommand.IconName),
			() => RuleFor(updateSocialMediaCommand => updateSocialMediaCommand.IconName)
				  .MaximumLength(FieldLimits.SocialMediaIconNameMaxLength)
				  .WithMessage($"IconName must be at most {FieldLimits.SocialMediaIconNameMaxLength} characters long.")
		);
		
		When
		(
			updateSocialMediaCommand => !string.IsNullOrEmpty(updateSocialMediaCommand.Link),
			() => RuleFor(updateSocialMediaCommand => updateSocialMediaCommand.Link)
				  .MaximumLength(FieldLimits.SocialMediaLinkMaxLength)
				  .WithMessage($"Link must be at most {FieldLimits.SocialMediaLinkMaxLength} characters long.")
		);
	}
}
