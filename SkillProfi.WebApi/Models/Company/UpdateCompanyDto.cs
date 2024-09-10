using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.Company.Command.Update;

namespace SkillProfi.WebApi.Models.Company;

public sealed class UpdateCompanyDto : IMapWith<UpdateCompanyCommand>
{
	public string? Name { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Address { get; set; }
	public string? DirectorName { get; set; }
	
	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateCompanyDto, UpdateCompanyCommand>()
			   .ForMember(command => command.Name, opt => opt.MapFrom(dto => dto.Name))
			   .ForMember(command => command.Email, opt => opt.MapFrom(dto => dto.Email))
			   .ForMember(command => command.PhoneNumber, opt => opt.MapFrom(dto => dto.PhoneNumber))
			   .ForMember(command => command.Address, opt => opt.MapFrom(dto => dto.Address))
			   .ForMember(command => command.DirectorName, opt => opt.MapFrom(dto => dto.DirectorName));
	}
}
