using MediatR;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.ClientRequests.Command.Create;

public sealed class CreateClientRequestCommand : IRequest<int>
{
	public string ClientName { get; set; } = string.Empty;
	public string ClientEmail { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
	public ClientRequestStatus Status { get; set; }
}