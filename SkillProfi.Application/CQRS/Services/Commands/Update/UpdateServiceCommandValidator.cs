using FluentValidation;
using SkillProfi.Application.Common;
using SkillProfi.Application.Interfaces;

namespace SkillProfi.Application.CQRS.Services.Commands.Update;

public sealed class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
	public UpdateServiceCommandValidator()
	{
		RuleFor(updateServiceCommand => updateServiceCommand.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");

		When
		(
			updateServiceCommand => !string.IsNullOrEmpty(updateServiceCommand.Title),
			() => RuleFor(updateServiceCommand => updateServiceCommand.Title)
				  .MaximumLength(FieldLimits.ServiceTitleMaxLength)
				  .WithMessage($"Title must be at most {FieldLimits.ServiceTitleMaxLength} characters long.")
		);

		When
		(
			updateServiceCommand => !string.IsNullOrEmpty(updateServiceCommand.Title),
			() => RuleFor(createServiceCommand => createServiceCommand.Description)
				  .MaximumLength(FieldLimits.ServiceDescriptionMaxLength)
				  .WithMessage($"Description must be at most {FieldLimits.ServiceDescriptionMaxLength} characters long.")
		);
	}
}
