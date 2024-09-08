using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.SiteItems.Commands.Update;

namespace SkillProfi.WebApi.Models.SiteItem;

public sealed class UpdateSiteItemModel : IMapWith<UpdateSiteItemCommand>
{
	public string Key { get; set; }
	public string Title { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateSiteItemModel, UpdateSiteItemCommand>()
			   .ForMember(updateSiteItemCommand => updateSiteItemCommand.Key, opt => opt.MapFrom(updateSiteItemModel => updateSiteItemModel.Key))
			   .ForMember(updateSiteItemCommand => updateSiteItemCommand.Title, opt => opt.MapFrom(updateSiteItemModel => updateSiteItemModel.Title));
	}
}
