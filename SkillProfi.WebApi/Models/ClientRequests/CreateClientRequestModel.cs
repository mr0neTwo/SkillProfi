using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.ClientRequests.Command.Create;

namespace SkillProfi.WebApi.Models.ClientRequests;

public sealed class CreateClientRequestModel : IMapWith<CreateClientRequestCommand>
{
	public string ClientName { get; set; } = string.Empty;
	public string ClientEmail { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
	
	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreateClientRequestModel, CreateClientRequestCommand>()
			   .ForMember(createClientRequestCommand => createClientRequestCommand.ClientName, opt => opt.MapFrom(createClientRequestModel => createClientRequestModel.ClientName))
			   .ForMember(createClientRequestCommand => createClientRequestCommand.ClientEmail, opt => opt.MapFrom(createClientRequestModel => createClientRequestModel.ClientEmail))
			   .ForMember(createClientRequestCommand => createClientRequestCommand.Message, opt => opt.MapFrom(createClientRequestModel => createClientRequestModel.Message));
	}
}