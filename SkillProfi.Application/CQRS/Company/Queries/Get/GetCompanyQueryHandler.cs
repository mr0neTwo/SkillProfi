using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Common.Exceptions;
using SkillProfi.Application.Interfaces;

namespace SkillProfi.Application.CQRS.Company.Queries.Get;

public sealed class GetCompanyQueryHandler(IAppContext appContext, IMapper mapper) : IRequestHandler<GetCompanyQuery, CompanyDto>
{
	public async Task<CompanyDto> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
	{
		Domain.Company? company = await appContext.Companies.FirstAsync(cancellationToken);

		if (company == null)
		{
			throw new NotFoundException(nameof(Domain.Company), "Company");
		}

		CompanyDto companyDto = mapper.Map<CompanyDto>(company);

		return companyDto;
	}
}
