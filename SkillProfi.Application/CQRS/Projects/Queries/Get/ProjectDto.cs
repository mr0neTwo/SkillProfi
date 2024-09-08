using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Projects.Queries.Get;

public sealed class ProjectDto : IMapWith<Project>
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string ImageUrl { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;

	public void Mapping(Profile profile)
	{
		profile.CreateMap<Project, ProjectDto>()
			   .ForMember(projectDto => projectDto.Id, opt => opt.MapFrom(project => project.Id))
			   .ForMember(projectDto => projectDto.Title, opt => opt.MapFrom(project => project.Title))
			   .ForMember(projectDto => projectDto.ImageUrl, opt => opt.MapFrom(project => project.ImageUrl))
			   .ForMember(projectDto => projectDto.Description, opt => opt.MapFrom(project => project.Description));
	}
}
