using MediatR;

namespace SkillProfi.Application.CQRS.SiteItems.Commands.Delete;

public sealed class DeleteSiteItemCommand : IRequest<Unit>
{
	public string Key { get; set; }
}