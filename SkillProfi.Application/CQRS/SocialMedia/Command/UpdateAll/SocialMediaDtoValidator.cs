using FluentValidation;
using SkillProfi.Application.Common;
using SkillProfi.Application.CQRS.SocialMedia.Queries.GetList;

namespace SkillProfi.Application.CQRS.SocialMedia.Command.UpdateAll;

public sealed class SocialMediaDtoValidator : AbstractValidator<SocialMediaDto>
{
	public SocialMediaDtoValidator()
	{
		RuleFor(socialMedia => socialMedia.IconName)
			.NotEmpty()
			.WithMessage("IconName is required.")
			.MaximumLength(FieldLimits.SocialMediaIconNameMaxLength)
			.WithMessage($"IconName must be at most {FieldLimits.SocialMediaIconNameMaxLength} characters long.");

		RuleFor(socialMedia => socialMedia.Link)
			.NotEmpty()
			.WithMessage("Link is required.")
			.MaximumLength(FieldLimits.SocialMediaLinkMaxLength)
			.WithMessage($"Link must be at most {FieldLimits.SocialMediaLinkMaxLength} characters long.");
	}
}
