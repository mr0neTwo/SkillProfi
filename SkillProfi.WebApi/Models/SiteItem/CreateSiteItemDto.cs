using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.SiteItems.Commands.Create;

namespace SkillProfi.WebApi.Models.SiteItem;

public sealed class CreateSiteItemDto : IMapWith<CreateSiteItemCommand>
{
	public string Key { get; set; }
	public string Title { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreateSiteItemDto, CreateSiteItemCommand>()
			   .ForMember(command => command.Key, opt => opt.MapFrom(dto => dto.Key))
			   .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title));
	}
}