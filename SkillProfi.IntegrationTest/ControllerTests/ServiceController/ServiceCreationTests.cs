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
public sealed class ServiceCreationTests(SkillProfiApplicationFactory<Program> factory) : TestBase<Service>(factory)
{
	[Fact]
	public async Task CreateService_Success()
	{
		// Arrange
		Service testService = TestServiceData.TestService1;
		
		CreateServiceDto request = new()
		{
			Title = testService.Title, 
			Description = testService.Description
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/service/Create", request);

			// Assert
			response.EnsureSuccessStatusCode();
			int serviceId = await response.Content.ReadFromJsonAsync<int>();

			Service? service = await GetEntityByIdAsync(serviceId);
			service.Should().NotBeNull();
			service!.Title.Should().Be(request.Title);
			service.Description.Should().Be(request.Description);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task CreateService_FailedByEmptyFields()
	{
		// Arrange
		CreateServiceDto request = new();
		
		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/service/Create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			string[,] errors = 
			{
				{ "Title", "Title is required." },
				{ "Description", "Description is required." }
			};

			AssertErrors(jsonResponse, errors);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task CreateService_FailedByOverLimitFields()
	{
		// Arrange
		CreateServiceDto request = new()
		{
			Title = new string('x', FieldLimits.ServiceTitleMaxLength + 1),  
			Description = new string('x', FieldLimits.ServiceDescriptionMaxLength + 1)
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/service/Create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
			
			string[,] errors = 
			{
				{ "Title", $"Title must be at most {FieldLimits.ServiceTitleMaxLength} characters long." },
				{ "Description", $"Description must be at most {FieldLimits.ServiceDescriptionMaxLength} characters long." }
			};

			AssertErrors(jsonResponse, errors);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}