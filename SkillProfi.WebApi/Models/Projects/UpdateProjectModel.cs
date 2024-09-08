using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.Projects.Command.Update;

namespace SkillProfi.WebApi.Models.Projects;

public sealed class UpdateProjectModel : IMapWith<UpdateProjectCommand>
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? ImageBase64 { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateProjectModel, UpdateProjectCommand>()
			   .ForMember(command => command.Id, opt => opt.MapFrom(model => model.Id))
			   .ForMember(command => command.Title, opt => opt.MapFrom(model => model.Title))
			   .ForMember(command => command.Description, opt => opt.MapFrom(model => model.Description));
	}
}
