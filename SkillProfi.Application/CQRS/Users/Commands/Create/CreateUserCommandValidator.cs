using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	private readonly IAppContext _appContext;

	public CreateUserCommandValidator(IAppContext appContext)
	{
		_appContext = appContext;

		RuleFor(createUserCommand => createUserCommand.Name)
			.NotEmpty()
			.WithMessage("Name is required.")
			.MaximumLength(FieldLimits.UserNameMaxLength)
			.WithMessage($"Name must be at least {FieldLimits.UserNameMaxLength} characters long.");
		
		RuleFor(createUserCommand => createUserCommand.Email)
			.NotEmpty()
			.WithMessage("Email is required.")
			.MaximumLength(FieldLimits.UserEmailMaxLength)
			.WithMessage($"Email must be at least {FieldLimits.UserEmailMaxLength} characters long.")
			.MustAsync(IsEmailUnique)
			.WithMessage("Email already exists.");
		
		RuleFor(createUserCommand => createUserCommand.Password)
			.NotEmpty()
			.WithMessage("Password is required.")
			.MinimumLength(FieldLimits.UserPasswordMinLength)
			.WithMessage($"Password must be at least {FieldLimits.UserPasswordMinLength} characters long.")
			.Matches(@"[A-Z]")
			.WithMessage("Password must contain at least one uppercase letter.")
			.Matches(@"[a-z]")
			.WithMessage("Password must contain at least one lowercase letter.")
			.Matches(@"[0-9]")
			.WithMessage("Password must contain at least one number.")
			.Matches(@"[\!\?\*\.]")
			.WithMessage("Password must contain at least one special character (!?*.).");
	}
	
	private async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken)
	{
		User? user = await _appContext.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

		return user == null;
	}
}
