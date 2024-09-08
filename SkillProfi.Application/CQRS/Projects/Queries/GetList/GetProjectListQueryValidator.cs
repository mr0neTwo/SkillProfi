using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.Projects.Queries.GetList;

public sealed class GetProjectListQueryValidator : AbstractValidator<GetProjectListQuery>
{
	public GetProjectListQueryValidator()
	{
		RuleFor(query => query.PageNumber)
			.GreaterThan(0)
			.WithMessage("Page number must be greater than 0.");

		RuleFor(query => query.PageSize)
			.GreaterThan(0)
			.WithMessage("Page size must be greater than 0.")
			.LessThanOrEqualTo(FieldLimits.MaxItemsPerPage)
			.WithMessage($"Page size must be less than or equal to {FieldLimits.MaxItemsPerPage}.");
	}
}
