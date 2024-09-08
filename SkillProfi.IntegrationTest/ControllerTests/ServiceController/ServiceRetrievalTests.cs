using System.Net.Http.Json;
using FluentAssertions;
using Flurl;
using SkillProfi.Application.CQRS.Services.Queries.GetList;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.ServiceController;

[Collection(nameof(ApiTestCollection))]
public sealed class ServiceRetrievalTests(SkillProfiApplicationFactory<Program> factory) 
	: TestBase<Service>(factory)
{
	[Fact]
	public async Task GetProjectList_Success()
	{
		// Arrange
		Service[] services = 
		[
			TestServiceData.TestService1, 
			TestServiceData.TestService2, 
			TestServiceData.TestService3
		];

		await AddEntitiesAsync(services);
		
		GetServiceListQuery query = new()
		{
			PageNumber = 1,
			PageSize = 50
		};
		
		Url requestUri = "api/Service/GetList".SetQueryParams(query);

		try
		{
			//Act
			HttpResponseMessage response = await Client.GetAsync(requestUri);

			// Assert
			response.EnsureSuccessStatusCode();
			GetServiceListResponse? serviceListResponse = await response.Content.ReadFromJsonAsync<GetServiceListResponse>();
			serviceListResponse.Should().NotBeNull();
			serviceListResponse!.PageNumber.Should().Be(1);
			serviceListResponse.Count.Should().Be(services.Length);
			
			for (int i = 0; i < serviceListResponse.Services.Count; i++)
			{
				serviceListResponse.Services[i].Title.Should().Be(services[i].Title);
				serviceListResponse.Services[i].Description.Should().Be(services[i].Description);
			}
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}
