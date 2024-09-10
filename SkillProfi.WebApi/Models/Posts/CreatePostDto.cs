using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.Posts.Command.Create;

namespace SkillProfi.WebApi.Models.Posts;

public sealed class CreatePostDto : IMapWith<CreatePostCommand>
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string? ImageBase64 { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreatePostDto, CreatePostCommand>()
			   .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title))
			   .ForMember(command => command.Description, opt => opt.MapFrom(dto => dto.Description));
	}
}