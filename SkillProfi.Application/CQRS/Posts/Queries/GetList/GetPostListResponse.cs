namespace SkillProfi.Application.CQRS.Posts.Queries.GetList;

public sealed class GetPostListResponse
{
	public List<PostDto> Posts { get; set; }
	public int Count { get; set; }
	public int PageNumber { get; set; }
	public int TotalPages { get; set; }
}
