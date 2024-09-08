using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.Projects.Command.Create;

namespace SkillProfi.WebApi.Models.Projects;

public sealed class CreateProjectModel : IMapWith<CreateProjectCommand>
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string? ImageBase64 { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreateProjectModel, CreateProjectCommand>()
			   .ForMember(command => command.Title, opt => opt.MapFrom(model => model.Title))
			   .ForMember(command => command.Description, opt => opt.MapFrom(model => model.Description));
	}
}