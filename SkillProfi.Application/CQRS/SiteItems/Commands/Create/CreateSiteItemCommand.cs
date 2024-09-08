using MediatR;

namespace SkillProfi.Application.CQRS.SiteItems.Commands.Create;

public sealed class CreateSiteItemCommand : IRequest<int>
{
	public string Key { get; set; }
	public string Title { get; set; }
	public int CreatedBy { get; set; }
}
