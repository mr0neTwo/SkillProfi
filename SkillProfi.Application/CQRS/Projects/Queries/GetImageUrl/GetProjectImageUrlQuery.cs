using MediatR;

namespace SkillProfi.Application.CQRS.Projects.Queries.GetImageUrl;

public sealed class GetProjectImageUrlQuery : IRequest<string?>
{
	public int Id { get; set; }
}