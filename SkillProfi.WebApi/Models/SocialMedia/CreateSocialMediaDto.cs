using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.SocialMedia.Command.Create;

namespace SkillProfi.WebApi.Models.SocialMedia;

public sealed class CreateSocialMediaDto : IMapWith<CreateSocialMediaCommand>
{
	public string IconName { get; set; } = string.Empty;
	public string Link { get; set; } = string.Empty;

	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreateSocialMediaDto, CreateSocialMediaCommand>()
			   .ForMember(command => command.Link, opt => opt.MapFrom(dto => dto.Link))
			   .ForMember(command => command.IconName, opt => opt.MapFrom(dto => dto.IconName));
	}
}
