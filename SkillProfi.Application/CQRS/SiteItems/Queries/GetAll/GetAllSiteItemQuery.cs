using MediatR;

namespace SkillProfi.Application.CQRS.SiteItems.Queries.GetAll;

public sealed class GetAllSiteItemQuery : IRequest<Dictionary<string, string>>
{
}