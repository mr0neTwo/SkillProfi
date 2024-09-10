using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.Posts.Command.Update;

namespace SkillProfi.WebApi.Models.Posts;

public sealed class UpdatePostDto : IMapWith<UpdatePostCommand>
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? ImageBase64 { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdatePostDto, UpdatePostCommand>()
			   .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.Id))
			   .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title))
			   .ForMember(command => command.Description, opt => opt.MapFrom(dto => dto.Description));
	}
}
