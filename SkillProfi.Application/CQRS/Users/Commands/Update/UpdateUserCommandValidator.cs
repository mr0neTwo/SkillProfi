using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Users.Commands.Update;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
	private readonly IAppContext _appContext;

	public UpdateUserCommandValidator(IAppContext appContext)
	{
		_appContext = appContext;

		RuleFor(updateUserCommand => updateUserCommand.Id)
			.GreaterThan(0)
			.WithMessage("Id must be a positive number.");
		
		When
		(
			updateUserCommand => !string.IsNullOrEmpty(updateUserCommand.Name), 
			() => RuleFor(updateUserCommand => updateUserCommand.Name)
				  .MaximumLength(FieldLimits.UserNameMaxLength)
				  .WithMessage($"Name must be at most {FieldLimits.UserNameMaxLength} characters long.")
		);
		
		When
		(
			updateUserCommand => !string.IsNullOrEmpty(updateUserCommand.Email), 
			() => RuleFor(updateUserCommand => updateUserCommand.Email)
				  .MaximumLength(FieldLimits.UserEmailMaxLength)
				  .WithMessage($"Email must be at most {FieldLimits.UserEmailMaxLength} characters long.")
				  .MustAsync(IsEmailUnique)
				  .WithMessage("Email already exists.")
		);
		
		When
		(
			updateUserCommand => !string.IsNullOrEmpty(updateUserCommand.Password), 
			() => RuleFor(updateUserCommand => updateUserCommand.Password)
				  .MinimumLength(FieldLimits.UserPasswordMinLength)
				  .WithMessage($"Password must be at most {FieldLimits.UserPasswordMinLength} characters long.")
				  .Matches(@"[A-Z]")
				  .WithMessage("Password must contain at least one uppercase letter.")
				  .Matches(@"[a-z]")
				  .WithMessage("Password must contain at least one lowercase letter.")
				  .Matches(@"[0-9]")
				  .WithMessage("Password must contain at least one number.")
				  .Matches(@"[\!\?\*\.]")
				  .WithMessage("Password must contain at least one special character (!?*.).")
		);
	}
	
	private async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken)
	{
		User? user = await _appContext.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

		return user == null;
	}
}
