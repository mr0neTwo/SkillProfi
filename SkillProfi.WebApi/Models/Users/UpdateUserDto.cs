using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.Users.Commands.Update;

namespace SkillProfi.WebApi.Models.Users;

public sealed class UpdateUserDto : IMapWith<UpdateUserCommand>
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Email { get; set; }
	public string? Password { get; set; }
	
	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
			   .ForMember(updateUserCommand => updateUserCommand.Id, opt => opt.MapFrom(userDto => userDto.Id))
			   .ForMember(updateUserCommand => updateUserCommand.Name, opt => opt.MapFrom(userDto => userDto.Name))
			   .ForMember(updateUserCommand => updateUserCommand.Email, opt => opt.MapFrom(userDto => userDto.Email))
			   .ForMember(updateUserCommand => updateUserCommand.Password, opt => opt.MapFrom(userDto => userDto.Password));
	}
}
