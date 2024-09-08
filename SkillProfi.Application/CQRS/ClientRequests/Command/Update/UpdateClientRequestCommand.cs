using MediatR;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.ClientRequests.Command.Update;

public sealed class UpdateClientRequestCommand : IRequest<Unit>
{
	public int Id { get; set; }
	public ClientRequestStatus Status { get; set; }
	public int UpdatedBy { get; set; }
}