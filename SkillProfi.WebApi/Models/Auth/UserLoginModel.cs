using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.Services.AuthService;

namespace SkillProfi.WebApi.Models.Auth;

public sealed class UserLoginModel : IMapWith<AuthenticationRequest>
{
	public string Email { get; set; }
	public string Password { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<UserLoginModel, AuthenticationRequest>()
			   .ForMember(authenticationQuery => authenticationQuery.Email, opt => opt.MapFrom(userLoginModel => userLoginModel.Email))
			   .ForMember(authenticationQuery => authenticationQuery.Password, opt => opt.MapFrom(userLoginModel => userLoginModel.Password));
	}
}
