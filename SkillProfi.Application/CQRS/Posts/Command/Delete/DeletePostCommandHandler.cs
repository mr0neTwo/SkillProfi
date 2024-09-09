using MediatR;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Posts.Command.Delete;

public sealed class DeletePostCommandHandler(IAppContext appContext) : IRequestHandler<DeletePostCommand, Unit>
{
	public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
	{
		Post? post = await appContext.Posts.FindAsync([request.Id], cancellationToken);

		if (post == null)
		{
			throw new NotFoundException(nameof(Post), request.Id);
		}

		appContext.Posts.Remove(post);
		await appContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
