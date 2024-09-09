using MediatR;

namespace SkillProfi.Application.CQRS.Company.Queries.Get;

public sealed class GetCompanyQuery : IRequest<CompanyDto>
{
}