using FluentAssertions;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.ClientRequestController;

[Collection(nameof(ApiTestCollection))]
public sealed class ClientRequestDeletingTests(SkillProfiApplicationFactory<Program> factory)
	: TestBase<ClientRequest>(factory)
{
	[Fact]
	public async Task DeleteClientRequest_Success()
	{
		// Arrange
		ClientRequest testClientRequest = TestClientRequestData.ClientRequest1;
		
		await AddEntitiesAsync(testClientRequest);
		
	
		// Act
		HttpResponseMessage response = await Client.DeleteAsync($"api/ClientRequest/Delete/{testClientRequest.Id}");
	
		// Assert
		response.EnsureSuccessStatusCode();
		
		ClientRequest? clientRequest = await GetEntityByIdAsync(testClientRequest.Id);
		clientRequest.Should().BeNull();
	
		await CleanEntitiesAsync();
	}
}
