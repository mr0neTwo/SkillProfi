using FluentValidation;

namespace SkillProfi.Application.CQRS.Projects.Command.Delete;

public sealed class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{
	public DeleteProjectCommandValidator()
	{
		RuleFor(deleteProjectCommand => deleteProjectCommand.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");
	}
}
