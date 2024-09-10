using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.Services.Commands.Create;

namespace SkillProfi.WebApi.Models.Services;

public sealed class CreateServiceDto : IMapWith<CreateServiceCommand>
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;

	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreateServiceDto, CreateServiceCommand>()
			   .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title))
			   .ForMember(command => command.Description, opt => opt.MapFrom(dto => dto.Description));
	}
}