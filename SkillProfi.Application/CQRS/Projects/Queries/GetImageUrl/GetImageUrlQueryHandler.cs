using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Projects.Queries.GetImageUrl;

public sealed class GetImageUrlQueryHandler(IAppContext appContext) : IRequestHandler<GetImageUrlQuery, string?>
{
	public async Task<string?> Handle(GetImageUrlQuery request, CancellationToken cancellationToken)
	{
		Project? project = await appContext.Projects.FirstOrDefaultAsync(project => project.Id == request.Id, cancellationToken);

		if (project == null)
		{
			throw new NotFoundException(nameof(Project), request.Id);
		}
		
		return project.ImageUrl;
	}
}
