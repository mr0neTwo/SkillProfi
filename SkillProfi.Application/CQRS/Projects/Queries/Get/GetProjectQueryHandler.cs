using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Projects.Queries.Get;

public sealed class GetProjectQueryHandler(IAppContext appContext, IMapper mapper) : IRequestHandler<GetProjectQuery, ProjectDto>
{
	public async Task<ProjectDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
	{
		Project? project = await appContext.Projects.FirstOrDefaultAsync(project => project.Id == request.Id, cancellationToken);

		if (project == null)
		{
			throw new NotFoundException(nameof(Project), request.Id);
		}

		ProjectDto projectDto = mapper.Map<ProjectDto>(project);

		return projectDto;
	}
}
