using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.SiteItems.Commands.Update;

public class UpdateSiteItemCommandValidator : AbstractValidator<UpdateSiteItemCommand>
{
	public UpdateSiteItemCommandValidator()
	{
		RuleFor(createSiteItemCommand => createSiteItemCommand.Title)
			.NotEmpty()
			.WithMessage("Title is required.")
			.MaximumLength(FieldLimits.SiteItemTitleMaxLength)
			.WithMessage($"Title must be at least {FieldLimits.SiteItemTitleMaxLength} characters long.");
	}
}
