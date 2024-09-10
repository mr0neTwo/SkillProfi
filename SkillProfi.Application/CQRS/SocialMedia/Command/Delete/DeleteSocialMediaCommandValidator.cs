using FluentValidation;

namespace SkillProfi.Application.CQRS.SocialMedia.Command.Delete;

public sealed class DeleteSocialMediaCommandValidator : AbstractValidator<DeleteSocialMediaCommand>
{
	public DeleteSocialMediaCommandValidator()
	{
		RuleFor(socialMediaCommand => socialMediaCommand.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");
	}
}
