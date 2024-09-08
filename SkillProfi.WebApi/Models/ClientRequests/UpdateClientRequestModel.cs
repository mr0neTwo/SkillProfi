using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.ClientRequests.Command.Update;
using SkillProfi.Domain;

namespace SkillProfi.WebApi.Models.ClientRequests;

public sealed class UpdateClientRequestModel : IMapWith<UpdateClientRequestCommand>
{
	public int Id { get; set; }
	public ClientRequestStatus Status { get; set; }
	
	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateClientRequestModel, UpdateClientRequestCommand>()
			   .ForMember(updateClientRequestCommand => updateClientRequestCommand.Id, opt => opt.MapFrom(updateClientRequestModel => updateClientRequestModel.Id))
			   .ForMember(updateClientRequestCommand => updateClientRequestCommand.Status, opt => opt.MapFrom(updateClientRequestModel => updateClientRequestModel.Status));
	}
}
