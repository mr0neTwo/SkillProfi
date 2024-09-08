using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.CQRS.Projects.Queries.Get;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Projects.Queries.GetList;

public sealed class GetProjectListQueryHandler(IAppContext appContext, IMapper mapper) : IRequestHandler<GetProjectListQuery, GetProjectListResponse>
{
	public async Task<GetProjectListResponse> Handle(GetProjectListQuery request, CancellationToken cancellationToken)
	{
		int count = await appContext.Projects.CountAsync(cancellationToken);
		int skip = (request.PageNumber - 1) * request.PageSize;

		List<Project> projects = await appContext.Projects
												 .OrderBy(project => project.Id)
												 .Skip(skip)
												 .Take(request.PageSize)
												 .ToListAsync(cancellationToken);

		List<ProjectDto> projectDtos = mapper.Map<List<ProjectDto>>(projects);

		return new GetProjectListResponse
		{
			Projects = projectDtos,
			PageNumber = request.PageNumber,
			Count = count,
			TotalPages = (int)Math.Ceiling((double)count / request.PageSize)
		};
	}
}
