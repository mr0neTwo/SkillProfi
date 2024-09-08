using FluentValidation;

namespace SkillProfi.Application.CQRS.ClientRequests.Command.Delete;

public sealed class DeleteClientRequestCommandValidator : AbstractValidator<DeleteClientRequestCommand>
{
	public DeleteClientRequestCommandValidator()
	{
		RuleFor(deleteClientRequestCommand => deleteClientRequestCommand.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");
	}
}
