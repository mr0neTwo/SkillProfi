namespace SkillProfi.Application.Services.AuthService;

public interface IAuthService
{
	Task<AuthResult> Authenticate(AuthenticationRequest request, CancellationToken cancellationToken);
}
