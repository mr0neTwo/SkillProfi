using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SkillProfi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BaseController : ControllerBase
{
	protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
	protected int UserId => GetUserId();
	
	private IMediator? _mediator;

	private int GetUserId()
	{
		string? userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;

		return int.TryParse(userIdClaim, out int userId) ? userId : 0;
	}
}