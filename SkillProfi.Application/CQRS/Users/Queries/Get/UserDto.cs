﻿using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Users.Queries.Get;

public sealed class UserDto : IMapWith<User>
{
	public int Id { get; set; }
	public long CreationDate { get; set; }
	public string? Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;

	public void Mapping(Profile profile)
	{
		profile.CreateMap<User, UserDto>()
			   .ForMember(userDto => userDto.Id, opt => opt.MapFrom(user => user.Id))
			   .ForMember(userDto => userDto.CreationDate, opt => opt.MapFrom(user => new DateTimeOffset(user.CreationDate).ToUnixTimeMilliseconds()))
			   .ForMember(userDto => userDto.Name, opt => opt.MapFrom(user => user.Name))
			   .ForMember(userDto => userDto.Email, opt => opt.MapFrom(user => user.Email));
	}
}
