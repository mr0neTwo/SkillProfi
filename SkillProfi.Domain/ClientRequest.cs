namespace SkillProfi.Domain;

public sealed class ClientRequest : Entity
{
	public string ClientName { get; set; } = string.Empty;
	public string ClientEmail { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
	public ClientRequestStatus Status { get; set; }
}