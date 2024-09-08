using FluentValidation;

namespace SkillProfi.Application.CQRS.Projects.Queries.Get;

public sealed class GetProjectQueryValidator : AbstractValidator<GetProjectQuery>
{
	public GetProjectQueryValidator()
	{
		RuleFor(getProjectQuery => getProjectQuery.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");
	}
}
