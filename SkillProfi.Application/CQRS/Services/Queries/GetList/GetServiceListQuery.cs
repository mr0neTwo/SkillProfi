using MediatR;

namespace SkillProfi.Application.CQRS.Services.Queries.GetList;

public sealed class GetServiceListQuery : IRequest<GetServiceListResponse>
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
}