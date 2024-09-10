using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.SocialMedia.Command.Create;

public sealed class CreateSocialMediaCommandValidator : AbstractValidator<CreateSocialMediaCommand>
{
	public CreateSocialMediaCommandValidator()
	{
		RuleFor(createSocialMediaCommand => createSocialMediaCommand.IconName)
			.NotEmpty()
			.WithMessage("IconName is required.")
			.MaximumLength(FieldLimits.SocialMediaIconNameMaxLength)
			.WithMessage($"IconName must be at most {FieldLimits.SocialMediaIconNameMaxLength} characters long.");

		RuleFor(createSocialMediaCommand => createSocialMediaCommand.Link)
			.NotEmpty()
			.WithMessage("Link is required.")
			.MaximumLength(FieldLimits.SocialMediaLinkMaxLength)
			.WithMessage($"Link must be at most {FieldLimits.SocialMediaLinkMaxLength} characters long.");
	}
}
