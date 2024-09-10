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
			   .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.Id))
			   .ForMember(command => command.Name, opt => opt.MapFrom(dto => dto.Name))
			   .ForMember(command => command.Email, opt => opt.MapFrom(dto => dto.Email))
			   .ForMember(command => command.Password, opt => opt.MapFrom(dto => dto.Password));
	}
}
