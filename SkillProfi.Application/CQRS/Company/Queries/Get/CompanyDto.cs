using AutoMapper;
using SkillProfi.Application.Common.Mapping;

namespace SkillProfi.Application.CQRS.Company.Queries.Get;

public sealed class CompanyDto : IMapWith<Domain.Company>
{
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public string DirectorName { get; set; } = string.Empty;

	public void Mapping(Profile profile)
	{
		profile.CreateMap<Domain.Company, CompanyDto>()
			   .ForMember(companyDto => companyDto.Name, opt => opt.MapFrom(company => company.Name))
			   .ForMember(companyDto => companyDto.Email, opt => opt.MapFrom(company => company.Email))
			   .ForMember(companyDto => companyDto.PhoneNumber, opt => opt.MapFrom(company => company.PhoneNumber))
			   .ForMember(companyDto => companyDto.Address, opt => opt.MapFrom(company => company.Address))
			   .ForMember(companyDto => companyDto.DirectorName, opt => opt.MapFrom(company => company.DirectorName));
	}
}
