using FluentValidation;

namespace SkillProfi.Application.CQRS.Users.Queries.Get;

public sealed class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
	public GetUserQueryValidator()
	{
		RuleFor(getUserQuery => getUserQuery.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");
	}
}
