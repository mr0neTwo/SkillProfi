using MediatR;

namespace SkillProfi.Application.CQRS.Posts.Command.Create;

public sealed class CreatePostCommand : IRequest<int>
{
	public string Title { get; set; } = string.Empty;
	public string? ImageUrl { get; set; }
	public string Description { get; set; } = string.Empty;
	public int CreatedBy { get; set; }
}