using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.SiteItems.Queries.Get;

public sealed class SiteItemDto : IMapWith<SiteItem>
{
	public string Key { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;

	public void Mapping(Profile profile)
	{
		profile.CreateMap<SiteItem, SiteItemDto>()
			   .ForMember(siteItemDto => siteItemDto.Key, opt => opt.MapFrom(siteItem => siteItem.Key))
			   .ForMember(siteItemDto => siteItemDto.Title, opt => opt.MapFrom(siteItem => siteItem.Title));
	}
}
