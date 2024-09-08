using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.SiteItems.Commands.Create;

public class CreateSiteItemValidator : AbstractValidator<CreateSiteItemCommand>
{
	public CreateSiteItemValidator()
	{
		RuleFor(createSiteItemCommand => createSiteItemCommand.Key)
			.NotNull()
			.NotEmpty()
			.WithMessage("Key is required.")
			.MaximumLength(FieldLimits.SiteItemKexMaxLength)
			.WithMessage($"Key must be at least {FieldLimits.SiteItemKexMaxLength} characters long.");
		
		RuleFor(createSiteItemCommand => createSiteItemCommand.Title)
			.NotNull()
			.NotEmpty()
			.WithMessage("Title is required.")
			.MaximumLength(FieldLimits.SiteItemTitleMaxLength)
			.WithMessage($"Title must be at least {FieldLimits.SiteItemTitleMaxLength} characters long.");
	}
}
