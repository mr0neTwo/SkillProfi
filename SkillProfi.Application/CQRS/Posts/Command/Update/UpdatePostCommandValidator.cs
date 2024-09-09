using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.Posts.Command.Update;

public sealed class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
	public UpdatePostCommandValidator()
	{
		RuleFor(updatePostCommand => updatePostCommand.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");

		When
		(
			updatePostCommand => !string.IsNullOrEmpty(updatePostCommand.Title),
			() => RuleFor(updatePostCommand => updatePostCommand.Title)
				  .MaximumLength(FieldLimits.PostTitleMaxLength)
				  .WithMessage($"Title must be at least {FieldLimits.PostTitleMaxLength} characters long.")
		);
		
		When
		(
			updatePostCommand => !string.IsNullOrEmpty(updatePostCommand.ImageUrl),
			() => RuleFor(updatePostCommand => updatePostCommand.ImageUrl)
				  .MaximumLength(FieldLimits.PostImageUrlMaxLength)
				  .WithMessage($"ImageUrl must be at least {FieldLimits.PostImageUrlMaxLength} characters long.")
		);

		When
		(
			updatePostCommand => !string.IsNullOrEmpty(updatePostCommand.Title),
			() => RuleFor(updatePostCommand => updatePostCommand.Description)
				  .MaximumLength(FieldLimits.PostDescriptionMaxLength)
				  .WithMessage($"Description must be at least {FieldLimits.PostDescriptionMaxLength} characters long.")
		);
	}
}
