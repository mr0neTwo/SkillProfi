using SkillProfi.Application.CQRS.Projects.Queries.Get;

namespace SkillProfi.Application.CQRS.Projects.Queries.GetList;

public sealed class GetProjectListResponse
{
	public List<ProjectDto> Projects { get; set; }
	public int Count { get; set; }
	public int PageNumber { get; set; }
	public int TotalPages { get; set; }
}
