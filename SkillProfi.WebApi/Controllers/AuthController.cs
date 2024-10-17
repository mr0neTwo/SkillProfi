using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Application.Common.Settings;
using SkillProfi.Application.CQRS.Users.Queries.Get;
using SkillProfi.Application.Services.AuthService;
using SkillProfi.WebApi.Models.Auth;

namespace SkillProfi.WebApi.Controllers;

public sealed class AuthController(IMapper mapper, JwtSettings jwtSettings, IAuthService authService) : BaseController
{
	/// <summary>
	/// User login.
	/// Authenticates a user based on the provided credentials.
	/// </summary>
	/// <param name="userLoginDto">User login data</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Authentication token or an error message</returns>
	/// <response code="200">Success</response>
	/// <response code="400">If the login data is incorrect</response>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto, CancellationToken cancellationToken)
	{
		AuthenticationRequest authenticationRequest = mapper.Map<AuthenticationRequest>(userLoginDto);
		AuthResult authResult = await authService.Authenticate(authenticationRequest, cancellationToken);
		
		AuthResponse authResponse = new()
		{
			User = authResult.User, 
			ErrorMessage = authResult.ErrorMessage, 
			Success = authResult.Success
		};

		if (authResult.Success)
		{
			var cookieOptions = new CookieOptions
			{
				// HttpOnly = true,
				// Secure = false, 
				// SameSite = SameSiteMode.Strict, 
				Expires = DateTime.UtcNow.AddHours(jwtSettings.AccessTokenExpirationHours)
			};

			HttpContext.Response.Cookies.Append(jwtSettings.CookieFieldName, authResult.Token, cookieOptions);
			
			return Ok(authResponse);
		}

		return BadRequest(authResponse);
	}
	
	/// <summary>
	/// User logout.
	/// Removes the authentication token from cookies.
	/// </summary>
	/// <returns>HTTP 204 (No Content) status</returns>
	/// <response code="204">Logout successful</response>
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

	/// <summary>
	/// Refresh token.
	/// Generates a new authentication token for the current user.
	/// </summary>
	/// <returns>A new authentication token or an error message</returns>
	/// <response code="200">Token refreshed successfully</response>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> Refresh()
	{
		if (UserId != 0)
		{
			GetUserQuery query = new() { Id = UserId };
			UserDto userDto = await Mediator.Send(query);
			
			AuthResult authResult = new()
			{
				User = userDto,
				Success = true
			};
			
			return Ok(authResult);
		}

		AuthResult badResult = new()
		{
			Success = false, 
			ErrorMessage = "User does not exist."
		};

		return Ok(badResult);
	}
}
