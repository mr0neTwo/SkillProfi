using MediatR;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Projects.Command.Delete;

public sealed class DeleteProjectCommandHandler(IAppContext appContext) : IRequestHandler<DeleteProjectCommand, Unit>
{
	public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
	{
		Project? project = await appContext.Projects.FindAsync([request.Id], cancellationToken);

		if (project == null)
		{
			throw new NotFoundException(nameof(Project), request.Id);
		}

		appContext.Projects.Remove(project);
		await appContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
