using MediatR;

namespace SkillProfi.Application.CQRS.SocialMedia.Command.Update;

public sealed class UpdateSocialMediaCommand : IRequest<Unit>
{
	public int Id { get; set; }
	public string? IconName { get; set; }
	public string? Link { get; set; }
	public int UpdatedById { get; set; }
}