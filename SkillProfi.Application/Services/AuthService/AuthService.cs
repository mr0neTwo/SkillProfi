using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.CQRS.Users.Queries.Get;
using SkillProfi.Application.Interfaces;
using SkillProfi.Application.Services.TokenService;
using SkillProfi.Domain;

namespace SkillProfi.Application.Services.AuthService;

public sealed class AuthService(IAppContext appContext, IMapper mapper, ITokenProvider tokenProvider) : IAuthService
{
	public async Task<AuthResponse> Authenticate(AuthenticationRequest request, CancellationToken cancellationToken)
	{
		User? user = await appContext.Users.FirstOrDefaultAsync(user => user.Email == request.Email, cancellationToken : cancellationToken);

		if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
		{
			return new AuthResponse()
			{
				Success = false, 
				ErrorMessage = "Incorrect UserName or Password"
			};
		}

		AuthResponse authResponse = new()
		{
			Success = true, 
			Token = tokenProvider.GenerateToken(user),
			User = mapper.Map<UserDto>(user)
		};

		return authResponse;
	}
}
