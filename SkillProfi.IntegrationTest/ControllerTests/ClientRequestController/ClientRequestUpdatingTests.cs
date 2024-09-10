using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using SkillProfi.WebApi.Models.ClientRequests;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.ClientRequestController;

[Collection(nameof(ApiTestCollection))]
public sealed class ClientRequestUpdatingTests(SkillProfiApplicationFactory<Program> factory)
	: TestBase<ClientRequest>(factory)
{
	[Fact]
	public async Task UpdateClientRequest_Success()
	{
		// Arrange
		ClientRequest clientRequest = TestClientRequestData.ClientRequest1;

		await AddEntitiesAsync(clientRequest);
		
		UpdateClientRequestDto request = new()
		{
			Id = clientRequest.Id,
			Status = ClientRequestStatus.Done
		};
		
		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/ClientRequest/Update", request);

			// Assert
			response.EnsureSuccessStatusCode();
			
			ClientRequest? updatedClientRequest = await GetEntityByIdAsync(clientRequest.Id);

			updatedClientRequest.Should().NotBeNull();
			updatedClientRequest!.Status.Should().Be(request.Status);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task UpdateClientRequest_FailedByWrongStatus()
	{
		// Arrange
		ClientRequest clientRequest = TestClientRequestData.ClientRequest1;

		await AddEntitiesAsync(clientRequest);

		int maxStatusValue = Enum.GetValues(typeof(ClientRequestStatus)).Length;

		UpdateClientRequestDto request = new()
		{
			Id = clientRequest.Id, 
			Status = (ClientRequestStatus)maxStatusValue + 1
		};
	
		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/ClientRequest/Update", request);
	
			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
	
			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
	
			((string)jsonResponse![0].PropertyName).Should().Be("Status");
			((string)jsonResponse[0].ErrorMessage).Should().Be($"Status should be in the range from 0 to {maxStatusValue}");
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}