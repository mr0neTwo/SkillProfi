using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.Company.Command.Update;

namespace SkillProfi.WebApi.Models.Company;

public class UpdateCompanyModel : IMapWith<UpdateCompanyCommand>
{
	public string? Name { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Address { get; set; }
	public string? DirectorName { get; set; }
	
	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateCompanyModel, UpdateCompanyCommand>()
			   .ForMember(command => command.Name, opt => opt.MapFrom(model => model.Name))
			   .ForMember(command => command.Email, opt => opt.MapFrom(model => model.Email))
			   .ForMember(command => command.PhoneNumber, opt => opt.MapFrom(model => model.PhoneNumber))
			   .ForMember(command => command.Address, opt => opt.MapFrom(model => model.Address))
			   .ForMember(command => command.DirectorName, opt => opt.MapFrom(model => model.DirectorName));
	}
}
