using FluentValidation;

namespace SkillProfi.Application.CQRS.Posts.Command.Delete;

public sealed class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
{
	public DeletePostCommandValidator()
	{
		RuleFor(deletePostCommand => deletePostCommand.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");
	}
}
