using MediatR;

namespace SkillProfi.Application.CQRS.Projects.Command.Delete;

public sealed class DeleteProjectCommand : IRequest<Unit>
{
	public int Id { get; set; }
}