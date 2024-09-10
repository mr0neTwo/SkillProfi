using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.SiteItems.Commands.Update;

namespace SkillProfi.WebApi.Models.SiteItem;

public sealed class UpdateSiteItemDto : IMapWith<UpdateSiteItemCommand>
{
	public string Key { get; set; }
	public string Title { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateSiteItemDto, UpdateSiteItemCommand>()
			   .ForMember(command => command.Key, opt => opt.MapFrom(dto => dto.Key))
			   .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title));
	}
}
