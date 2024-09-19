using FluentValidation;
using SkillProfi.Application.Common;

namespace SkillProfi.Application.CQRS.Company.Command.Update;

public sealed class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
	public UpdateCompanyCommandValidator()
	{
		When
		(
			updateCompanyCommand => !string.IsNullOrEmpty(updateCompanyCommand.Name),
			() => RuleFor(updateProjectCommand => updateProjectCommand.Name)
				  .MaximumLength(FieldLimits.CompanyNameMaxLength)
				  .WithMessage($"Name must be at most {FieldLimits.CompanyNameMaxLength} characters long.")
		);
		
		When
		(
			updateCompanyCommand => !string.IsNullOrEmpty(updateCompanyCommand.Email),
			() => RuleFor(updateCompanyCommand => updateCompanyCommand.Email)
				  .MaximumLength(FieldLimits.CompanyEmailMaxLength)
				  .WithMessage($"Email must be at most {FieldLimits.CompanyEmailMaxLength} characters long.")
		);

		When
		(
			updateCompanyCommand => !string.IsNullOrEmpty(updateCompanyCommand.PhoneNumber),
			() => RuleFor(updateCompanyCommand => updateCompanyCommand.PhoneNumber)
				  .MaximumLength(FieldLimits.CompanyPhoneMaxLength)
				  .WithMessage($"PhoneNumber must be at most {FieldLimits.CompanyPhoneMaxLength} characters long.")
		);
		
		When
		(
			updateCompanyCommand => !string.IsNullOrEmpty(updateCompanyCommand.Address),
			() => RuleFor(updateCompanyCommand => updateCompanyCommand.Address)
				  .MaximumLength(FieldLimits.CompanyAddressMaxLength)
				  .WithMessage($"Address must be at most {FieldLimits.CompanyAddressMaxLength} characters long.")
		);
		
		When
		(
			updateCompanyCommand => !string.IsNullOrEmpty(updateCompanyCommand.DirectorName),
			() => RuleFor(updateCompanyCommand => updateCompanyCommand.DirectorName)
				  .MaximumLength(FieldLimits.CompanyDirectorNameMaxLength)
				  .WithMessage($"DirectorName must be at most {FieldLimits.CompanyDirectorNameMaxLength} characters long.")
		);
		
		When
		(
			updateCompanyCommand => !string.IsNullOrEmpty(updateCompanyCommand.MapLink),
			() => RuleFor(updateCompanyCommand => updateCompanyCommand.MapLink)
				  .MaximumLength(FieldLimits.CompanyMapLinkMaxLength)
				  .WithMessage($"MapLink must be at most {FieldLimits.CompanyMapLinkMaxLength} characters long.")
		);
	}
}
