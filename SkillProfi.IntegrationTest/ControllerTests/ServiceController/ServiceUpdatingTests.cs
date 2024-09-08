using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SkillProfi.Application.Common;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using SkillProfi.WebApi.Models.Services;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.ServiceController;

[Collection(nameof(ApiTestCollection))]
public sealed class ServiceUpdatingTests(SkillProfiApplicationFactory<Program> factory)
	: TestBase<Service>(factory)
{
	[Fact]
	public async Task UpdateService_Success()
	{
		// Arrange
		Service service = TestServiceData.TestService1;

		await AddEntitiesAsync(service);
		
		UpdateServiceModel request = new()
		{
			Id = service.Id,
			Title = "Updated Title",
			Description = "Updated Description"
		};
		
		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/Service/Update", request);

			// Assert
			response.EnsureSuccessStatusCode();
			
			Service? updatedService = await GetEntityByIdAsync(service.Id);

			updatedService.Should().NotBeNull();
			updatedService!.Title.Should().Be(request.Title);
			updatedService.Description.Should().Be(request.Description);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task UpdateService_FailedByOverLimitFields()
	{
		// Arrange
		Service service = TestServiceData.TestService1;
		await AddEntitiesAsync(service);
		
		UpdateServiceModel request = new()
		{
			Id = service.Id,
			Title = new string('x', FieldLimits.ServiceTitleMaxLength + 1),  
			Description = new string('x', FieldLimits.ServiceDescriptionMaxLength + 1)
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/Service/Update", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			string[,] errors = 
			{
				{ "Title", $"Title must be at least {FieldLimits.ServiceTitleMaxLength} characters long." },
				{ "Description", $"Description must be at least {FieldLimits.ServiceDescriptionMaxLength} characters long." }
			};

			AssertErrors(jsonResponse, errors);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}