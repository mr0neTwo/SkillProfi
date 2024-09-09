using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Posts.Queries.GetList;

public sealed class GetPostListQueryHandler(IAppContext appContext, IMapper mapper)
	: IRequestHandler<GetPostListQuery, GetPostListResponse>
{
	public async Task<GetPostListResponse> Handle(GetPostListQuery request, CancellationToken cancellationToken)
	{
		int count = await appContext.Posts.CountAsync(cancellationToken);
		int skip = (request.PageNumber - 1) * request.PageSize;

		List<Post> posts = await appContext.Posts
										   .OrderBy(post => post.Id)
										   .Skip(skip)
										   .Take(request.PageSize)
										   .ToListAsync(cancellationToken);

		List<PostDto> postDtos = mapper.Map<List<PostDto>>(posts);

		return new GetPostListResponse
		{
			Posts = postDtos,
			PageNumber = request.PageNumber,
			Count = count,
			TotalPages = (int)Math.Ceiling((double)count / request.PageSize)
		};
	}
}
