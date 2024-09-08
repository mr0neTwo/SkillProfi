using MediatR;

namespace SkillProfi.Application.CQRS.Services.Commands.Create;

public sealed class CreateServiceCommand : IRequest<int>
{
	public string Title { get; set; }
	public string Description { get; set; }
	public int CreatedBy { get; set; }
}