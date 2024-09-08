using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.ClientRequests.Queries.Get;

public sealed class ClientRequestDto : IMapWith<ClientRequest>
{
	public int Id { get; set; }
	public long CreationDate { get; set; }
	public string ClientName { get; set; } = string.Empty;
	public string ClientEmail { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
	public ClientRequestStatus Status { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<ClientRequest, ClientRequestDto>()
			   .ForMember(clientRequestDto => clientRequestDto.Id, opt => opt.MapFrom(clientRequest => clientRequest.Id))
			   .ForMember(clientRequestDto => clientRequestDto.CreationDate, opt => opt.MapFrom(clientRequest => new DateTimeOffset(clientRequest.CreationDate).ToUnixTimeMilliseconds()))
			   .ForMember(clientRequestDto => clientRequestDto.ClientName, opt => opt.MapFrom(clientRequest => clientRequest.ClientName))
			   .ForMember(clientRequestDto => clientRequestDto.ClientEmail, opt => opt.MapFrom(clientRequest => clientRequest.ClientEmail))
			   .ForMember(clientRequestDto => clientRequestDto.Message, opt => opt.MapFrom(clientRequest => clientRequest.Message))
			   .ForMember(clientRequestDto => clientRequestDto.Status, opt => opt.MapFrom(clientRequest => clientRequest.Status));
	}
}
