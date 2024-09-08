using MediatR;

namespace SkillProfi.Application.CQRS.Projects.Queries.GetList;

public sealed class GetProjectListQuery : IRequest<GetProjectListResponse>
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
}