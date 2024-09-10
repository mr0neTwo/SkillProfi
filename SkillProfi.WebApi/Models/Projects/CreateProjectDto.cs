using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.Projects.Command.Create;

namespace SkillProfi.WebApi.Models.Projects;

public sealed class CreateProjectDto : IMapWith<CreateProjectCommand>
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string? ImageBase64 { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreateProjectDto, CreateProjectCommand>()
			   .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title))
			   .ForMember(command => command.Description, opt => opt.MapFrom(dto => dto.Description));
	}
}