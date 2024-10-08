using FluentValidation;

namespace SkillProfi.Application.CQRS.SiteItems.Commands.UpdateAll;

public class UpdateAllSiteItemCommandValidator : AbstractValidator<UpdateAllSiteItemCommand>
{
	public UpdateAllSiteItemCommandValidator()
	{
		RuleFor(command => command.SiteItemDictionary)
			.NotNull()
			.WithMessage("SiteItemDictionary cannot be null.")
			.NotEmpty()
			.WithMessage("SiteItemDictionary cannot be empty.");

		RuleForEach(command => command.SiteItemDictionary)
			.Must(kvp => !string.IsNullOrWhiteSpace(kvp.Key))
			.WithMessage("Keys in SiteItemDictionary cannot be null or whitespace.")
			.Must(kvp => !string.IsNullOrWhiteSpace(kvp.Value))
			.WithMessage("Values in SiteItemDictionary cannot be null or whitespace.");
	}
}
