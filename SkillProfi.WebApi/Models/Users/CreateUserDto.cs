using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.Users.Commands.Create;

namespace SkillProfi.WebApi.Models.Users;

public sealed class CreateUserDto : IMapWith<CreateUserCommand>
{
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	
	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreateUserDto, CreateUserCommand>()
			   .ForMember(command => command.Name, opt => opt.MapFrom(dto => dto.Name))
			   .ForMember(command => command.Email, opt => opt.MapFrom(dto => dto.Email))
			   .ForMember(command => command.Password, opt => opt.MapFrom(dto => dto.Password));
	}
}