using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.Services.Commands.Create;

namespace SkillProfi.WebApi.Models.Services;

public sealed class CreateServiceModel : IMapWith<CreateServiceCommand>
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;

	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreateServiceModel, CreateServiceCommand>()
			   .ForMember(command => command.Title, opt => opt.MapFrom(model => model.Title))
			   .ForMember(command => command.Description, opt => opt.MapFrom(model => model.Description));
	}
}