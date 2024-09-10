using FluentValidation;
using SkillProfi.Application.Common;
using SkillProfi.Application.Interfaces;

namespace SkillProfi.Application.CQRS.Services.Commands.Create;

public sealed class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
	public CreateServiceCommandValidator()
	{
		RuleFor(createServiceCommand => createServiceCommand.Title)
			.NotEmpty()
			.WithMessage("Title is required.")
			.MaximumLength(FieldLimits.ServiceTitleMaxLength)
			.WithMessage($"Title must be at most {FieldLimits.ServiceTitleMaxLength} characters long.");

		RuleFor(createServiceCommand => createServiceCommand.Description)
			.NotEmpty()
			.WithMessage("Description is required.")
			.MaximumLength(FieldLimits.ServiceDescriptionMaxLength)
			.WithMessage($"Description must be at most {FieldLimits.ServiceDescriptionMaxLength} characters long.");
	}
}
