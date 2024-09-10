using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.ClientRequests.Command.Create;

public class CreateClientRequestCommandValidator : AbstractValidator<CreateClientRequestCommand>
{
	public CreateClientRequestCommandValidator()
	{
		RuleFor(createClientRequestCommand => createClientRequestCommand.ClientName)
			.NotEmpty()
			.WithMessage("Client name is required.")
			.MaximumLength(FieldLimits.ClientRequestNameMaxLength)
			.WithMessage($"Client name must be at most {FieldLimits.ClientRequestNameMaxLength} characters long.");
		
		RuleFor(createClientRequestCommand => createClientRequestCommand.ClientEmail)
			.NotEmpty()
			.WithMessage("Client email is required.")
			.MaximumLength(FieldLimits.ClientRequestEmailMaxLength)
			.WithMessage($"Client email must be at most {FieldLimits.ClientRequestEmailMaxLength} characters long.");

		
		RuleFor(createClientRequestCommand => createClientRequestCommand.Message)
			.NotEmpty()
			.WithMessage("Message is required.")
			.MaximumLength(FieldLimits.ClientRequestMessageMaxLength)
			.WithMessage($"Message must be at most {FieldLimits.ClientRequestMessageMaxLength} characters long.");
		
		RuleFor(createClientRequestCommand => createClientRequestCommand.Status)
			.IsInEnum()
			.WithMessage("A valid status is required.");
	}
}
