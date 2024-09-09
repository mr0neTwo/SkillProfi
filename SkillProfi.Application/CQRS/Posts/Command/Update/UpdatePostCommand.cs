using MediatR;

namespace SkillProfi.Application.CQRS.Posts.Command.Update;

public sealed class UpdatePostCommand : IRequest<Unit>
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public string? ImageUrl { get; set; }
	public string? Description { get; set; }
	public int UpdatedById { get; set; }
}