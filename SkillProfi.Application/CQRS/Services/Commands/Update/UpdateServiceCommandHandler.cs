using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Services.Commands.Update;

public sealed class UpdateServiceCommandHandler(IAppContext appContext) : IRequestHandler<UpdateServiceCommand, Unit>
{
	public async Task<Unit> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
	{
		Service? service = await appContext.Services.FirstOrDefaultAsync(service => service.Id == request.Id, cancellationToken);

		if (service == null)
		{
			throw new NotFoundException(nameof(Service), request.Id);
		}

		if (request.Title != null)
		{
			service.Title = request.Title;
		}

		if (request.Description != null)
		{
			service.Description = request.Description;
		}

		service.UpdatingDate = DateTime.Now;
		service.UpdatedById = request.UpdatedBy;

		appContext.Services.Update(service);
		await appContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
