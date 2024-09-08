namespace SkillProfi.Application.CQRS.Services.Queries.GetList;

public sealed class GetServiceListResponse
{
	public List<ServiceDto> Services { get; set; }
	public int PageNumber { get; set; }
	public int Count { get; set; }
	public int TotalPages { get; set; }
}
