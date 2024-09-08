namespace SkillProfi.Application.Services.AuthService;

public interface IAuthService
{
	Task<AuthResponse> Authenticate(AuthenticationRequest request, CancellationToken cancellationToken);
}
