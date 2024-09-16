using AutoMapper;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Posts.Queries.GetList;

public sealed class PostDto : IMapWith<Post>
{
	public int Id { get; set; }
	public long CreationDate { get; set; }
	public string Title { get; set; } = string.Empty;
	public string? ImageUrl { get; set; }
	public string Description { get; set; } = string.Empty;

	public void Mapping(Profile profile)
	{
		profile.CreateMap<Post, PostDto>()
			   .ForMember(postDto => postDto.Id, opt => opt.MapFrom(post => post.Id))
			   .ForMember(postDto => postDto.CreationDate, opt => opt.MapFrom(post => new DateTimeOffset(post.CreationDate).ToUnixTimeMilliseconds()))
			   .ForMember(postDto => postDto.Title, opt => opt.MapFrom(post => post.Title))
			   .ForMember(postDto => postDto.ImageUrl, opt => opt.MapFrom(post => post.ImageUrl))
			   .ForMember(postDto => postDto.Description, opt => opt.MapFrom(post => post.Description));
	}
}
