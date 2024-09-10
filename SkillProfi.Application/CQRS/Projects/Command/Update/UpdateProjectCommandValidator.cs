using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.Projects.Command.Update;

public sealed class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
	public UpdateProjectCommandValidator()
	{
		RuleFor(updateProjectCommand => updateProjectCommand.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");

		When
		(
			updateProjectCommand => !string.IsNullOrEmpty(updateProjectCommand.Title),
			() => RuleFor(updateProjectCommand => updateProjectCommand.Title)
				  .MaximumLength(FieldLimits.ProjectTitleMaxLength)
				  .WithMessage($"Title must be at most {FieldLimits.ProjectTitleMaxLength} characters long.")
		);
		
		When
		(
			updateProjectCommand => !string.IsNullOrEmpty(updateProjectCommand.ImageUrl),
			() => RuleFor(updateProjectCommand => updateProjectCommand.ImageUrl)
				  .MaximumLength(FieldLimits.ProjectImageUrlMaxLength)
				  .WithMessage($"ImageUrl must be at most {FieldLimits.ProjectImageUrlMaxLength} characters long.")
		);

		When
		(
			updateProjectCommand => !string.IsNullOrEmpty(updateProjectCommand.Title),
			() => RuleFor(updateProjectCommand => updateProjectCommand.Description)
				  .MaximumLength(FieldLimits.ProjectDescriptionMaxLength)
				  .WithMessage($"Description must be at most {FieldLimits.ProjectDescriptionMaxLength} characters long.")
		);
	}
}
