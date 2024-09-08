using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.Services.Commands.Update;

namespace SkillProfi.WebApi.Models.Services;

public sealed class UpdateServiceModel : IMapWith<UpdateServiceCommand>
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;

	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateServiceModel, UpdateServiceCommand>()
			   .ForMember(command => command.Id, opt => opt.MapFrom(model => model.Id))
			   .ForMember(command => command.Title, opt => opt.MapFrom(model => model.Title))
			   .ForMember(command => command.Description, opt => opt.MapFrom(model => model.Description));
	}
}
