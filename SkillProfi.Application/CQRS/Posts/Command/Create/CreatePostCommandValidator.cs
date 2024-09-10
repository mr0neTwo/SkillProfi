using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.Posts.Command.Create;

public sealed class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
	public CreatePostCommandValidator()
	{
		RuleFor(createPostCommand => createPostCommand.Title)
			.NotEmpty()
			.WithMessage("Title is required.")
			.MaximumLength(FieldLimits.PostTitleMaxLength)
			.WithMessage($"Title must be at most {FieldLimits.PostTitleMaxLength} characters long.");
		
		When
		(
			createPostCommand => !string.IsNullOrEmpty(createPostCommand.ImageUrl),
			() => RuleFor(createPostCommand => createPostCommand.ImageUrl)
				  .MaximumLength(FieldLimits.PostImageUrlMaxLength)
				  .WithMessage($"ImageUrl must be at most {FieldLimits.PostImageUrlMaxLength} characters long.")
		);

		RuleFor(createPostCommand => createPostCommand.Description)
			.NotEmpty()
			.WithMessage("Description is required.")
			.MaximumLength(FieldLimits.PostDescriptionMaxLength)
			.WithMessage($"Description must be at most {FieldLimits.PostDescriptionMaxLength} characters long.");
	}
}
