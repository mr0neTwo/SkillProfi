using MediatR;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Projects.Command.Create;

public sealed class CreateProjectCommandHandler(IAppContext appContext) : IRequestHandler<CreateProjectCommand, int>
{
	public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
	{
		Project project = new()
		{
			Title = request.Title,
			Description = request.Description,
			ImageUrl = request.ImageUrl,
			CreationDate = DateTime.Now,
			CreatedById = request.CreatedBy
		};

		await appContext.Projects.AddAsync(project, cancellationToken);
		await appContext.SaveChangesAsync(cancellationToken);

		return project.Id;
	}
}
