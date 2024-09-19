using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Interfaces;

namespace SkillProfi.Application.CQRS.SocialMedia.Queries.GetList;

public sealed class GetSocialMediaListQueryHandler(IAppContext appContext, IMapper mapper)
	: IRequestHandler<GetSocialMediaListQuery, List<SocialMediaDto>>
{
	public async Task<List<SocialMediaDto>> Handle(GetSocialMediaListQuery request, CancellationToken cancellationToken)
	{
		List<Domain.SocialMedia> socialMedias = await appContext.SocialMedias
																.OrderBy(socialMedia => socialMedia.Id)
																.ToListAsync(cancellationToken);

		List<SocialMediaDto> socialMediaDtos = mapper.Map<List<SocialMediaDto>>(socialMedias);

		return socialMediaDtos;
	}
}
