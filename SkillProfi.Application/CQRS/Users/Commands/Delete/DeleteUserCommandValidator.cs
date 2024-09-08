using FluentValidation;

namespace SkillProfi.Application.CQRS.Users.Commands.Delete;

public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
	public DeleteUserCommandValidator()
	{
		RuleFor(deleteUserCommand => deleteUserCommand.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");
	}
}
