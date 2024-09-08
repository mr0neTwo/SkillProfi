using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Flurl;
using Newtonsoft.Json;
using SkillProfi.Application.Common;
using SkillProfi.Application.CQRS.ClientRequests.Queries.Get;
using SkillProfi.Application.CQRS.ClientRequests.Queries.GetList;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.ClientRequestController;

[Collection(nameof(ApiTestCollection))]
public sealed class ClientRequestRetrievalTests(SkillProfiApplicationFactory<Program> factory) 
	: TestBase<ClientRequest>(factory)
{
	[Fact]
	public async Task GetClientRequest_Success()
	{
		// Arrange
		ClientRequest clientRequest = TestClientRequestData.ClientRequest1;
		await AddEntitiesAsync(clientRequest);
		
		try
		{
			//Act
			HttpResponseMessage response = await Client.GetAsync($"api/ClientRequest/Get/{clientRequest.Id}");

			// Assert
			response.EnsureSuccessStatusCode();
			ClientRequestDto? clientRequestDto = await response.Content.ReadFromJsonAsync<ClientRequestDto>();
			clientRequestDto.Should().NotBeNull();
			clientRequestDto!.ClientName.Should().Be(clientRequest.ClientName);
			clientRequestDto.ClientEmail.Should().Be(clientRequest.ClientEmail);
			clientRequestDto.Message.Should().Be(clientRequest.Message);
		}
		finally
		{
			await CleanEntitiesAsync();
		} 
	}
	
	[Fact]
	public async Task GetClientRequestList_Success()
	{
		// Arrange
		ClientRequest[] clientRequests = 
		[
			TestClientRequestData.ClientRequest1, 
			TestClientRequestData.ClientRequest2, 
			TestClientRequestData.ClientRequest3, 
		];

		await AddEntitiesAsync(clientRequests);

		GetClientRequestListQuery query = new()
		{
			StartTimestamp = new DateTimeOffset(clientRequests.First().CreationDate).ToUnixTimeSeconds(),
			EndTimeStamp = new DateTimeOffset(clientRequests.Last().CreationDate).ToUnixTimeSeconds(),
			PageNumber = 1,
			PageSize = 50
		};
		
		Url requestUri = "api/ClientRequest/GetList".SetQueryParams(query);
		
		try
		{
			//Act
			HttpResponseMessage response = await Client.GetAsync(requestUri);

			// Assert
			response.EnsureSuccessStatusCode();
			GetClientRequestResponse? responseBody = await response.Content.ReadFromJsonAsync<GetClientRequestResponse>();
			responseBody.Should().NotBeNull();
			responseBody!.PageNumber.Should().Be(1);
			responseBody.Count.Should().Be(clientRequests.Length);

			for (int i = 0; i < responseBody.Count; i++)
			{
				responseBody.ClientRequests[i].ClientName.Should().Be(clientRequests[i].ClientName);
				responseBody.ClientRequests[i].ClientEmail.Should().Be(clientRequests[i].ClientEmail);
				responseBody.ClientRequests[i].Message.Should().Be(clientRequests[i].Message);
			}
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task GetClientRequestList_FailedByWrongTimestampRange()
	{
		// Arrange
		ClientRequest[] clientRequests = 
		[
			TestClientRequestData.ClientRequest1, 
			TestClientRequestData.ClientRequest2, 
			TestClientRequestData.ClientRequest3, 
		];

		await AddEntitiesAsync(clientRequests);
		
		GetClientRequestListQuery query = new()
		{
			StartTimestamp = long.MaxValue,
			EndTimeStamp = long.MinValue,
			PageNumber = 1,
			PageSize = 50
		};
		
		Url requestUri = "api/ClientRequest/GetList".SetQueryParams(query);
		
		try
		{
			//Act
			HttpResponseMessage response = await Client.GetAsync(requestUri);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);			
			
			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			((string)jsonResponse![0].PropertyName).Should().Be("StartTimestamp");
			((string)jsonResponse[0].ErrorMessage).Should().Be($"StartTimestamp must be less than or equal to {FieldLimits.MaxTimestamp}.");
			((string)jsonResponse[1].PropertyName).Should().Be("EndTimeStamp");
			((string)jsonResponse[1].ErrorMessage).Should().Be($"EndTimeStamp must be greater than or equal to {FieldLimits.MinTimestamp}.");
			((string)jsonResponse[2].ErrorMessage).Should().Be("EndTimeStamp must be greater than or equal to StartTimestamp.");
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}