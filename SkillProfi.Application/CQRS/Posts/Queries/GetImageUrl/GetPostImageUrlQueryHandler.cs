using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Posts.Queries.GetImageUrl;

public sealed class GetPostImageUrlQueryHandler(IAppContext appContext) : IRequestHandler<GetPostImageUrlQuery, string?>
{
	public async Task<string?> Handle(GetPostImageUrlQuery request, CancellationToken cancellationToken)
	{
		Post? post = await appContext.Posts.FirstOrDefaultAsync(post => post.Id == request.Id, cancellationToken);

		if (post == null)
		{
			throw new NotFoundException(nameof(Post), request.Id);
		}
		
		return post.ImageUrl;
	}
}
