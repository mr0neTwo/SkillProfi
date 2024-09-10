using MediatR;

namespace SkillProfi.Application.CQRS.SocialMedia.Command.Create;

public sealed class CreateSocialMediaCommand : IRequest<int>
{
	public string IconName { get; set; } = string.Empty;
	public string Link { get; set; } = string.Empty;
	public int CreatedById { get; set; }
}