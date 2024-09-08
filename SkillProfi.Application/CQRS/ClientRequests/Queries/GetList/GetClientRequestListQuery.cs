using MediatR;

namespace SkillProfi.Application.CQRS.ClientRequests.Queries.GetList;

public sealed class GetClientRequestListQuery : IRequest<GetClientRequestResponse>
{
	public long StartTimestamp { get; set; }
	public long EndTimeStamp { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
}