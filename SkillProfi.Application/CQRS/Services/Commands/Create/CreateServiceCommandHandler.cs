using MediatR;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;

namespace SkillProfi.Application.CQRS.Services.Commands.Create;

public sealed class CreateServiceCommandHandler(IAppContext appContext) : IRequestHandler<CreateServiceCommand, int>
{
	public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
	{
		Service service = new()
		{
			Title = request.Title,
			Description = request.Description,
			CreationDate = DateTime.Now,
			CreatedById = request.CreatedBy
		};

		await appContext.Services.AddAsync(service, cancellationToken);
		await appContext.SaveChangesAsync(cancellationToken);

		return service.Id;
	}
}
