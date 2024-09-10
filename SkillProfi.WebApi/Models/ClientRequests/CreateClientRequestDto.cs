using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.ClientRequests.Command.Create;

namespace SkillProfi.WebApi.Models.ClientRequests;

public sealed class CreateClientRequestDto : IMapWith<CreateClientRequestCommand>
{
	public string ClientName { get; set; } = string.Empty;
	public string ClientEmail { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
	
	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreateClientRequestDto, CreateClientRequestCommand>()
			   .ForMember(command => command.ClientName, opt => opt.MapFrom(dto => dto.ClientName))
			   .ForMember(command => command.ClientEmail, opt => opt.MapFrom(dto => dto.ClientEmail))
			   .ForMember(command => command.Message, opt => opt.MapFrom(dto => dto.Message));
	}
}