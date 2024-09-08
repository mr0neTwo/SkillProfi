using MediatR;

namespace SkillProfi.Application.CQRS.ClientRequests.Queries.Get;

public sealed class GetClientRequestQuery : IRequest<ClientRequestDto>
{
	public int Id { get; set; }
}