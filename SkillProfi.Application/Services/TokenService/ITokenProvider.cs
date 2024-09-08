using SkillProfi.Domain;

namespace SkillProfi.Application.Services.TokenService;

public interface ITokenProvider
{
	string GenerateToken(User user);
}
