using MediatR;

namespace SkillProfi.Application.CQRS.SiteItems.Commands.UpdateAll;

public sealed class UpdateAllSiteItemCommand : IRequest<Unit>
{
	public Dictionary<string, string> SiteItemDictionary { get; set; }
	public int UpdatedById { get; set; }
}