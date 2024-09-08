using MediatR;

namespace SkillProfi.Application.CQRS.Projects.Queries.Get;

public sealed class GetProjectQuery : IRequest<ProjectDto>
{
	public int Id { get; set; }
}