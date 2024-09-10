using MediatR;
using SkillProfi.Application.CQRS.SocialMedia.Queries.GetList;

namespace SkillProfi.Application.CQRS.SocialMedia.Command.UpdateAll;

public sealed class UpdateAllSocialMediaCommand : IRequest<Unit>
{
	public List<SocialMediaDto> SocialMedias { get; set; }
	public int UpdatedById { get; set; }
}