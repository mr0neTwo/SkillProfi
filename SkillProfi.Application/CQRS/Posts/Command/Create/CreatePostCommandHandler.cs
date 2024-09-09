using MediatR;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Posts.Command.Create;

public sealed class CreatePostCommandHandler(IAppContext appContext) : IRequestHandler<CreatePostCommand, int>
{
	public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
	{
		Post post = new()
		{
			Title = request.Title,
			Description = request.Description,
			ImageUrl = request.ImageUrl,
			CreationDate = DateTime.Now,
			CreatedById = request.CreatedBy
		};

		await appContext.Posts.AddAsync(post, cancellationToken);
		await appContext.SaveChangesAsync(cancellationToken);

		return post.Id;
	}
}
