using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.Services.AuthService;

namespace SkillProfi.WebApi.Models.Auth;

public sealed class UserLoginDto : IMapWith<AuthenticationRequest>
{
	public string Email { get; set; }
	public string Password { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<UserLoginDto, AuthenticationRequest>()
			   .ForMember(query => query.Email, opt => opt.MapFrom(dto => dto.Email))
			   .ForMember(query => query.Password, opt => opt.MapFrom(dto => dto.Password));
	}
}
