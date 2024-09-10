using MediatR;

namespace SkillProfi.Application.CQRS.SocialMedia.Command.Delete;

public sealed class DeleteSocialMediaCommand : IRequest<Unit>
{
	public int Id { get; set; }
}