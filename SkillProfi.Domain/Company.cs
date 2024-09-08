namespace SkillProfi.Domain;

public sealed class Company : Entity
{
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public string DirectorName { get; set; } = string.Empty;
}