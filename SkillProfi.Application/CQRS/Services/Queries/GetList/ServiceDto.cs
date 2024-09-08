using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Services.Queries.GetList;

public sealed class ServiceDto : IMapWith<Service>
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Service, ServiceDto>()
			   .ForMember(serviceDto => serviceDto.Id, opt => opt.MapFrom(service => service.Id))
			   .ForMember(serviceDto => serviceDto.Title, opt => opt.MapFrom(service => service.Title))
			   .ForMember(serviceDto => serviceDto.Description, opt => opt.MapFrom(service => service.Description));
	}
}
