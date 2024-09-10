using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.SiteItems.Queries.Get;

public sealed class GetSiteItemQueryValidator : AbstractValidator<GetSiteItemQuery>
{
	public GetSiteItemQueryValidator()
	{
		RuleFor(createSiteItemCommand => createSiteItemCommand.Key)
			.NotEmpty()
			.WithMessage("Key is required.")
			.MaximumLength(FieldLimits.SiteItemKexMaxLength)
			.WithMessage($"Key must be at most {FieldLimits.SiteItemKexMaxLength} characters long.");
	}
}
