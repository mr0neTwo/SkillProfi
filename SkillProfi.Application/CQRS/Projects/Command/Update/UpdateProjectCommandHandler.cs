using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Projects.Command.Update;

public sealed class UpdateProjectCommandHandler(IAppContext appContext) : IRequestHandler<UpdateProjectCommand, Unit>
{
	public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
	{
		Project? project = await appContext.Projects.FirstOrDefaultAsync(project => project.Id == request.Id, cancellationToken);

		if (project == null)
		{
			throw new NotFoundException(nameof(Project), request.Id);
		}

		if (request.Title != null)
		{
			project.Title = request.Title;
		}
		
		if (request.ImageUrl != null)
		{
			project.ImageUrl = request.ImageUrl;
		}

		if (request.Description != null)
		{
			project.Description = request.Description;
		}

		project.UpdatingDate = DateTime.Now;
		project.UpdatedById = request.UpdatedById;

		appContext.Projects.Update(project);
		await appContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
