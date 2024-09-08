using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SkillProfi.Application.Common.Settings;
using SkillProfi.Domain;

namespace SkillProfi.Application.Services.TokenService;

public sealed class TokenProvider(JwtSettings jwtSettings) : ITokenProvider
{
	public string GenerateToken(User user)
	{
		SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(jwtSettings.AccessTokenSecret));
		SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

		Claim[] claims =
		{
			new("Id", user.Id.ToString()),
			new(JwtRegisteredClaimNames.Sub, user.Email),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		JwtSecurityToken token = new
		(
			issuer : jwtSettings.Issuer,
			audience : jwtSettings.Audience,
			claims : claims, 
			expires : DateTime.UtcNow.AddHours(jwtSettings.AccessTokenExpirationHours),
			signingCredentials : credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
