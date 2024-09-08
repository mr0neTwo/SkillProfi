using FluentValidation;

namespace SkillProfi.Application.CQRS.Services.Commands.Delete;

public sealed class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
{
	public DeleteServiceCommandValidator()
	{
		RuleFor(deleteServiceCommand => deleteServiceCommand.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");
	}
}
