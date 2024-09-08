using MediatR;

namespace SkillProfi.Application.CQRS.SiteItems.Queries.Get;

public sealed class GetSiteItemQuery : IRequest<SiteItemDto>
{
	public string Key { get; set; }
}