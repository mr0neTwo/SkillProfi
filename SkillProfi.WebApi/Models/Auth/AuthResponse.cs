using SkillProfi.Application.CQRS.Users.Queries.Get;

namespace SkillProfi.WebApi.Models.Auth;

public class AuthResponse
{
	public UserDto User { get; set; }
	public string ErrorMessage { get; set; }
	public bool Success { get; set; }
}
