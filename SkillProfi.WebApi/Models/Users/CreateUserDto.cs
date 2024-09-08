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
			   .ForMember(createUserCommand => createUserCommand.Name, opt => opt.MapFrom(userDto => userDto.Name))
			   .ForMember(createUserCommand => createUserCommand.Email, opt => opt.MapFrom(userDto => userDto.Email))
			   .ForMember(createUserCommand => createUserCommand.Password, opt => opt.MapFrom(userDto => userDto.Password));
	}
}