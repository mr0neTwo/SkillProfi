using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Application.Common.Settings;
using SkillProfi.Application.CQRS.Users.Queries.Get;
using SkillProfi.Application.Services.AuthService;
using SkillProfi.WebApi.Models.Auth;

namespace SkillProfi.WebApi.Controllers;

public sealed class AuthController(IMapper mapper, JwtSettings jwtSettings, IAuthService authService) : BaseController
{
	[HttpPost]
	public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginModel, CancellationToken cancellationToken)
	{
		AuthenticationRequest authenticationRequest = mapper.Map<AuthenticationRequest>(userLoginModel);
		AuthResponse authResponse = await authService.Authenticate(authenticationRequest, cancellationToken);

		if (authResponse.Success)
		{
			var cookieOptions = new CookieOptions
			{
				// HttpOnly = true,
				// Secure = false, 
				// SameSite = SameSiteMode.Strict, 
				Expires = DateTime.UtcNow.AddHours(jwtSettings.AccessTokenExpirationHours)
			};

			HttpContext.Response.Cookies.Append(jwtSettings.CookieFieldName, authResponse.Token, cookieOptions);
			
			return Ok(authResponse);
		}

		return BadRequest(authResponse);
	}
	
	[HttpGet]
	public IActionResult Logout()
	{
		if (Request.Cookies.ContainsKey(jwtSettings.CookieFieldName))
		{
			CookieOptions cookieOptions = new()
			{
				Expires = DateTime.UtcNow.AddDays(-1)
			};
        
			Response.Cookies.Append(jwtSettings.CookieFieldName, string.Empty, cookieOptions);
		}

		return NoContent();
	}

	[HttpGet]
	public async Task<IActionResult> Refresh()
	{
		if (UserId != 0)
		{
			GetUserQuery query = new() { Id = UserId };
			UserDto userDto = await Mediator.Send(query);
			
			AuthResponse authResponse = new()
			{
				User = userDto,
				Success = true
			};
			
			return Ok(authResponse);
		}

		AuthResponse badResponse = new()
		{
			Success = false, 
			ErrorMessage = "User does not exist."
		};

		return Ok(badResponse);
	}
}
