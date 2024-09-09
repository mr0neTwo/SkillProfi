using MediatR;

namespace SkillProfi.Application.CQRS.Posts.Queries.GetImageUrl;

public class GetPostImageUrlQuery : IRequest<string>
{
	public int Id { get; set; }
}