using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.Projects.Command.Create;

public sealed class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
	public CreateProjectCommandValidator()
	{
		RuleFor(createProjectCommand => createProjectCommand.Title)
			.NotEmpty()
			.WithMessage("Title is required.")
			.MaximumLength(FieldLimits.ProjectTitleMaxLength)
			.WithMessage($"Title must be at most {FieldLimits.ProjectTitleMaxLength} characters long.");
		
		When
		(
			updateProjectCommand => !string.IsNullOrEmpty(updateProjectCommand.ImageUrl),
			() => RuleFor(updateProjectCommand => updateProjectCommand.ImageUrl)
				  .MaximumLength(FieldLimits.ProjectImageUrlMaxLength)
				  .WithMessage($"ImageUrl must be at most {FieldLimits.ProjectImageUrlMaxLength} characters long.")
		);

		RuleFor(createProjectCommand => createProjectCommand.Description)
			.NotEmpty()
			.WithMessage("Description is required.")
			.MaximumLength(FieldLimits.ProjectDescriptionMaxLength)
			.WithMessage($"Description must be at most {FieldLimits.ProjectDescriptionMaxLength} characters long.");
	}
}
