using AutoMapper;
using SkillProfi.Application.Common.Mapping;

namespace SkillProfi.Application.CQRS.SocialMedia.Queries.GetList;

public sealed class SocialMediaDto : IMapWith<Domain.SocialMedia>
{
	public int Id { get; set; }
	public string IconName { get; set; } = string.Empty;
	public string Link { get; set; } = string.Empty;

	public void Mapping(Profile profile)
	{
		profile.CreateMap<Domain.SocialMedia, SocialMediaDto>()
			   .ForMember(socialMediaDto => socialMediaDto.Id, opt => opt.MapFrom(socialMedia => socialMedia.Id))
			   .ForMember(socialMediaDto => socialMediaDto.IconName, opt => opt.MapFrom(socialMedia => socialMedia.IconName))
			   .ForMember(socialMediaDto => socialMediaDto.Link, opt => opt.MapFrom(socialMedia => socialMedia.Link));
	}
}
