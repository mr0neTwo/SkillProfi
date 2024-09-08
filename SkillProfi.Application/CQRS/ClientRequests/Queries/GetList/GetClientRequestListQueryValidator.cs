using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.ClientRequests.Queries.GetList;

public sealed class GetClientRequestListQueryValidator : AbstractValidator<GetClientRequestListQuery>
{
	public GetClientRequestListQueryValidator()
	{
		RuleFor(query => query.StartTimestamp)
			.NotNull()
			.WithMessage("StartTimestamp is required.")
			.GreaterThanOrEqualTo(FieldLimits.MinTimestamp)
			.WithMessage($"StartTimestamp must be greater than or equal to {FieldLimits.MinTimestamp}.")
			.LessThanOrEqualTo(FieldLimits.MaxTimestamp)
			.WithMessage($"StartTimestamp must be less than or equal to {FieldLimits.MaxTimestamp}.");

		RuleFor(query => query.EndTimeStamp)
			.NotNull()
			.WithMessage("EndTimeStamp is required.")
			.GreaterThanOrEqualTo(FieldLimits.MinTimestamp)
			.WithMessage($"EndTimeStamp must be greater than or equal to {FieldLimits.MinTimestamp}.")
			.LessThanOrEqualTo(FieldLimits.MaxTimestamp)
			.WithMessage($"EndTimeStamp must be less than or equal to {FieldLimits.MaxTimestamp}.");

		RuleFor(query => query)
			.Must(query => query.EndTimeStamp >= query.StartTimestamp)
			.WithMessage("EndTimeStamp must be greater than or equal to StartTimestamp.");
		
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
