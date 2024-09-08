using SkillProfi.Application.CQRS.ClientRequests.Queries.Get;

namespace SkillProfi.Application.CQRS.ClientRequests.Queries.GetList;

public sealed class GetClientRequestResponse
{
	public List<ClientRequestDto> ClientRequests { get; set; }
	public int PageNumber { get; set; }
	public int Count { get; set; }
	public int TotalPages { get; set; }
}
