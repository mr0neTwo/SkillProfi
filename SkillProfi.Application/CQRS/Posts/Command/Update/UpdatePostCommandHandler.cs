using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Posts.Command.Update;

public sealed class UpdatePostCommandHandler(IAppContext appContext) : IRequestHandler<UpdatePostCommand, Unit>
{
	public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
	{
		Post? post = await appContext.Posts.FirstOrDefaultAsync(post => post.Id == request.Id, cancellationToken);

		if (post == null)
		{
			throw new NotFoundException(nameof(Post), request.Id);
		}

		if (request.Title != null)
		{
			post.Title = request.Title;
		}
		
		if (request.ImageUrl != null)
		{
			post.ImageUrl = request.ImageUrl;
		}

		if (request.Description != null)
		{
			post.Description = request.Description;
		}

		post.UpdatingDate = DateTime.Now;
		post.UpdatedById = request.UpdatedById;

		appContext.Posts.Update(post);
		await appContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
