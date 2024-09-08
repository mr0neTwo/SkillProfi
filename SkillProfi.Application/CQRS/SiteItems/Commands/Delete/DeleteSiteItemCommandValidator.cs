using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.SiteItems.Commands.Delete;

public sealed class DeleteSiteItemCommandValidator : AbstractValidator<DeleteSiteItemCommand>
{
	public DeleteSiteItemCommandValidator()
	{
		RuleFor(createSiteItemCommand => createSiteItemCommand.Key)
			.NotEmpty()
			.WithMessage("Key is required.")
			.MaximumLength(FieldLimits.SiteItemKexMaxLength)
			.WithMessage($"Key must be at least {FieldLimits.SiteItemKexMaxLength} characters long.");
	}
}
