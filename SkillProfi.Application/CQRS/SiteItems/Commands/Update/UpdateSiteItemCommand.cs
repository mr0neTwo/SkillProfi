using MediatR;

namespace SkillProfi.Application.CQRS.SiteItems.Commands.Update;

public sealed class UpdateSiteItemCommand : IRequest<Unit>
{
	public string Key { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public int UpdatedById { get; set; }
}
