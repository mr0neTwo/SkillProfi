using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.CQRS.Posts.Command.Create;

namespace SkillProfi.WebApi.Models.Posts;

public sealed class CreatePostModel : IMapWith<CreatePostCommand>
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string? ImageBase64 { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<CreatePostModel, CreatePostCommand>()
			   .ForMember(command => command.Title, opt => opt.MapFrom(model => model.Title))
			   .ForMember(command => command.Description, opt => opt.MapFrom(model => model.Description));
	}
}