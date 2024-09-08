using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.SiteItems.Commands.Create;

namespace SkillProfi.WebApi.Models.SiteItem;

public sealed class CreateSiteItemModel : IMapWith<CreateSiteItemCommand>
{
	public string Key { get; set; }
	public string Title { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreateSiteItemModel, CreateSiteItemCommand>()
			   .ForMember(createSiteItemCommand => createSiteItemCommand.Key, opt => opt.MapFrom(createSiteItemModel => createSiteItemModel.Key))
			   .ForMember(createSiteItemCommand => createSiteItemCommand.Title, opt => opt.MapFrom(createSiteItemModel => createSiteItemModel.Title));
	}
}