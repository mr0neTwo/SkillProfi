using System.Net.Http.Json;
using FluentAssertions;
using SkillProfi.Application.CQRS.Company.Queries.Get;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.CompanyController;

[Collection(nameof(ApiTestCollection))]
public sealed class CompanyRetrievalTests(SkillProfiApplicationFactory<Program> factory) : TestBase<Company>(factory)
{
	[Fact]
	public async Task GetCompany_Success()
	{
		// Arrange
		Company company = TestCompanyData.Company;

		await AddEntitiesAsync(company);
		
		try
		{
			//Act
			HttpResponseMessage response = await Client.GetAsync($"api/Company/Get");

			// Assert
			response.EnsureSuccessStatusCode();
			CompanyDto? companyDto = await response.Content.ReadFromJsonAsync<CompanyDto>();
			companyDto.Should().NotBeNull();
			companyDto!.Name.Should().Be(company.Name);
			companyDto.Email.Should().Be(company.Email);
			companyDto.PhoneNumber.Should().Be(company.PhoneNumber);
			companyDto.Address.Should().Be(company.Address);
			companyDto.DirectorName.Should().Be(company.DirectorName);
			companyDto.MapLink.Should().Be(company.MapLink);
		}
		finally
		{
			await CleanEntitiesAsync();
		} 
	}
}
