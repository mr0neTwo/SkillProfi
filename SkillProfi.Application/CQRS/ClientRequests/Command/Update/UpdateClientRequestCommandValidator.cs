using FluentValidation;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.ClientRequests.Command.Update;

public sealed class UpdateClientRequestCommandValidator : AbstractValidator<UpdateClientRequestCommand>
{
	public UpdateClientRequestCommandValidator()
	{
		RuleFor(updateClientRequestCommand => updateClientRequestCommand.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");
		
		RuleFor(updateClientRequestCommand => updateClientRequestCommand.Status)
			.IsInEnum()
			.WithMessage($"Status should be in the range from 0 to {Enum.GetValues(typeof(ClientRequestStatus)).Length}");
	}
}
