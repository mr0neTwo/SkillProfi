using MediatR;

namespace SkillProfi.Application.CQRS.Services.Commands.Update;

public sealed class UpdateServiceCommand : IRequest<Unit>
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public int UpdatedBy { get; set; }
}