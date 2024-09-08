using FluentValidation;

namespace SkillProfi.Application.CQRS.ClientRequests.Queries.Get;

public sealed class GetClientRequestQueryValidator : AbstractValidator<GetClientRequestQuery>
{
	public GetClientRequestQueryValidator()
	{
		RuleFor(getUserQuery => getUserQuery.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");
	}
}
