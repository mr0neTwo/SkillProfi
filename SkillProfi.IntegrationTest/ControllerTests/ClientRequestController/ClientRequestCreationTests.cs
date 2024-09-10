using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SkillProfi.Application.Common;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using SkillProfi.WebApi.Models.ClientRequests;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.ClientRequestController;

[Collection(nameof(ApiTestCollection))]
public sealed class ClientRequestCreationTests(SkillProfiApplicationFactory<Program> factory) : TestBase<ClientRequest>(factory)
{
	[Fact]
	public async Task CreateClientRequest_Success()
	{
		// Arrange
		CreateClientRequestDto request = new()
		{
			ClientName = TestClientRequestData.ClientRequest1.ClientName, 
			ClientEmail = TestClientRequestData.ClientRequest1.ClientEmail, 
			Message = TestClientRequestData.ClientRequest1.Message
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/ClientRequest/Create", request);

			// Assert
			response.EnsureSuccessStatusCode();
			int clientRequestId = await response.Content.ReadFromJsonAsync<int>();

			ClientRequest? user = await GetEntityByIdAsync(clientRequestId);
			user.Should().NotBeNull();
			user!.ClientName.Should().Be(request.ClientName);
			user.ClientEmail.Should().Be(request.ClientEmail);
			user.Message.Should().Be(request.Message);
			user.Status.Should().Be(ClientRequestStatus.Received);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task CreateClientRequest_FailedByEmptyFields()
	{
		// Arrange
		CreateClientRequestDto request = new();

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/ClientRequest/Create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			((string)jsonResponse![0].PropertyName).Should().Be("ClientName");
			((string)jsonResponse[0].ErrorMessage).Should().Be("Client name is required.");
			((string)jsonResponse[1].PropertyName).Should().Be("ClientEmail");
			((string)jsonResponse[1].ErrorMessage).Should().Be("Client email is required.");
			((string)jsonResponse[2].PropertyName).Should().Be("Message");
			((string)jsonResponse[2].ErrorMessage).Should().Be("Message is required.");
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task CreateClientRequest_FailedByOverLimitFields()
	{
		// Arrange
		CreateClientRequestDto request = new()
		{
			ClientName = new string('x', FieldLimits.ClientRequestNameMaxLength + 1), 
			ClientEmail = new string('x', FieldLimits.ClientRequestEmailMaxLength + 1), 
			Message = new string('x', FieldLimits.ClientRequestMessageMaxLength + 1)
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/ClientRequest/Create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			((string)jsonResponse![0].PropertyName).Should().Be("ClientName");
			((string)jsonResponse[0].ErrorMessage).Should().Be($"Client name must be at most {FieldLimits.ClientRequestNameMaxLength} characters long.");
			((string)jsonResponse[1].PropertyName).Should().Be("ClientEmail");
			((string)jsonResponse[1].ErrorMessage).Should().Be($"Client email must be at most {FieldLimits.ClientRequestEmailMaxLength} characters long.");
			((string)jsonResponse[2].PropertyName).Should().Be("Message");
			((string)jsonResponse[2].ErrorMessage).Should().Be($"Message must be at most {FieldLimits.ClientRequestMessageMaxLength} characters long.");
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}