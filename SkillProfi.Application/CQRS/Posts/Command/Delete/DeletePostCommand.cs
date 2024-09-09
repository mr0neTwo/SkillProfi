using MediatR;

namespace SkillProfi.Application.CQRS.Posts.Command.Delete;

public sealed class DeletePostCommand : IRequest<Unit>
{
	public int Id { get; set; }
}