using MediatR;

namespace SkillProfi.Application.CQRS.Company.Command.Update;

public sealed class UpdateCompanyCommand : IRequest<Unit>
{
	public string? Name { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Address { get; set; }
	public string? DirectorName { get; set; }
	public int UpdatedById { get; set; }
}