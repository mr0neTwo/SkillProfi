using MediatR;

namespace SkillProfi.Application.Services.AuthService;

public class AuthenticationRequest : IRequest<AuthResponse>
{
	public string Email { get; set; }
	public string Password { get; set; }
}
