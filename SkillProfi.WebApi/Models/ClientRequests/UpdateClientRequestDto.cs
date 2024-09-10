using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.ClientRequests.Command.Update;
using SkillProfi.Domain;

namespace SkillProfi.WebApi.Models.ClientRequests;

public sealed class UpdateClientRequestDto : IMapWith<UpdateClientRequestCommand>
{
	public int Id { get; set; }
	public ClientRequestStatus Status { get; set; }
	
	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateClientRequestDto, UpdateClientRequestCommand>()
			   .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.Id))
			   .ForMember(command => command.Status, opt => opt.MapFrom(dto => dto.Status));
	}
}
