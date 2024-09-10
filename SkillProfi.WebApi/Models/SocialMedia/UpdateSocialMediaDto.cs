using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.SocialMedia.Command.Update;

namespace SkillProfi.WebApi.Models.SocialMedia;

public sealed class UpdateSocialMediaDto : IMapWith<UpdateSocialMediaCommand>
{
	public int Id { get; set; }
	public string? IconName { get; set; }
	public string? Link { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateSocialMediaDto, UpdateSocialMediaCommand>()
			   .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.Id))
			   .ForMember(command => command.Link, opt => opt.MapFrom(dto => dto.Link))
			   .ForMember(command => command.IconName, opt => opt.MapFrom(dto => dto.IconName));
	}
}