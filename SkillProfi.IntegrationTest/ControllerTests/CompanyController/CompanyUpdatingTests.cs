using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SkillProfi.Application.Common;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using SkillProfi.WebApi.Models.Company;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.CompanyController;

[Collection(nameof(ApiTestCollection))]
public sealed class CompanyUpdatingTests(SkillProfiApplicationFactory<Program> factory)
	: TestBase<Company>(factory)
{
	[Fact]
	public async Task UpdateCompany_Success()
	{
		// Arrange
		Company company = TestCompanyData.Company;

		await AddEntitiesAsync(company);
		
		UpdateCompanyModel request = new()
		{
			Name = "Updated name", 
			Email = "Updated email", 
			PhoneNumber = "Updated number", 
			Address = "Updated address", 
			DirectorName = "Updated Director"
		};
		
		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/Company/Update", request);

			// Assert
			response.EnsureSuccessStatusCode();
			
			Company? updatedCompany = await GetEntityByIdAsync(company.Id);

			updatedCompany.Should().NotBeNull();
			updatedCompany!.Name.Should().Be(request.Name);
			updatedCompany.Email.Should().Be(request.Email);
			updatedCompany.PhoneNumber.Should().Be(request.PhoneNumber);
			updatedCompany.Address.Should().Be(request.Address);
			updatedCompany.DirectorName.Should().Be(request.DirectorName);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task UpdateCompany_FailedByOverLimitFields()
	{
		// Arrange
		Company company = TestCompanyData.Company;
		await AddEntitiesAsync(company);
		
		UpdateCompanyModel request = new()
		{
			Name = new string('x', FieldLimits.CompanyNameMaxLength + 1),  
			Email = new string('x', FieldLimits.CompanyEmailMaxLength + 1),
			PhoneNumber = new string('x', FieldLimits.CompanyPhoneMaxLength + 1),
			Address = new string('x', FieldLimits.CompanyAddressMaxLength + 1),
			DirectorName = new string('x', FieldLimits.CompanyDirectorNameMaxLength + 1),
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/Company/Update", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			string[,] errors = 
			{
				{ "Name", $"Name must be at least {FieldLimits.CompanyNameMaxLength} characters long." },
				{ "Email", $"Email must be at least {FieldLimits.CompanyEmailMaxLength} characters long." },
				{ "PhoneNumber", $"PhoneNumber must be at least {FieldLimits.CompanyPhoneMaxLength} characters long." },
				{ "Address", $"Address must be at least {FieldLimits.CompanyAddressMaxLength} characters long." },
				{ "DirectorName", $"DirectorName must be at least {FieldLimits.CompanyDirectorNameMaxLength} characters long." },
			};

			AssertErrors(jsonResponse, errors);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}