using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;

namespace SkillProfi.Application.CQRS.Company.Command.Update;

public sealed class UpdateCompanyCommandHandler(IAppContext appContext) : IRequestHandler<UpdateCompanyCommand, Unit>
{
	public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
	{
		Domain.Company? company = await appContext.Companies.FirstAsync(cancellationToken);

		if (company == null)
		{
			throw new NotFoundException(nameof(Domain.Company), "Company");
		}

		if (request.Name != null)
		{
			company.Name = request.Name;
		}
		
		if (request.Email != null)
		{
			company.Email = request.Email;
		}

		if (request.PhoneNumber != null)
		{
			company.PhoneNumber = request.PhoneNumber;
		}
		
		if (request.Address != null)
		{
			company.Address = request.Address;
		}
		
		if (request.DirectorName != null)
		{
			company.DirectorName = request.DirectorName;
		}

		company.UpdatingDate = DateTime.Now;
		company.UpdatedById = request.UpdatedById;

		appContext.Companies.Update(company);
		await appContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
