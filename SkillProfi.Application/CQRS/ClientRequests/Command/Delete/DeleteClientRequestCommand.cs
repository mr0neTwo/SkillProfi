using MediatR;

namespace SkillProfi.Application.CQRS.ClientRequests.Command.Delete;

public sealed class DeleteClientRequestCommand : IRequest<Unit>
{
	public int Id { get; set; }
}