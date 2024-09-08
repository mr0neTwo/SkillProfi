using MediatR;

namespace SkillProfi.Application.CQRS.Services.Commands.Delete;

public sealed class DeleteServiceCommand : IRequest<Unit>
{
	public int Id { get; set; }
}