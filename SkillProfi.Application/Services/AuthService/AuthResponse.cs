using SkillProfi.Application.CQRS.Users.Queries.Get;

namespace SkillProfi.Application.Services.AuthService;

public sealed class AuthResponse
{
	public UserDto User { get; set; }
	public string Token { get; set; }
	public string ErrorMessage { get; set; }
	public bool Success { get; set; }
}
