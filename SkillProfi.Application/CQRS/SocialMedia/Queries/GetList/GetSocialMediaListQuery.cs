using MediatR;

namespace SkillProfi.Application.CQRS.SocialMedia.Queries.GetList;

public class GetSocialMediaListQuery : IRequest<List<SocialMediaDto>>
{
}