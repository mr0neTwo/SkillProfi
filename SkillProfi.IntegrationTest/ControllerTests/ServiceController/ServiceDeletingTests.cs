using FluentAssertions;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.ServiceController;

[Collection(nameof(ApiTestCollection))]
public sealed class ServiceDeletingTests(SkillProfiApplicationFactory<Program> factory)
	: TestBase<Service>(factory)
{
	[Fact]
	public async Task DeleteService_Success()
	{
		// Arrange
		Service service = TestServiceData.TestService1;
		
		await AddEntitiesAsync(service);
		
		// Act
		HttpResponseMessage response = await Client.DeleteAsync($"api/Service/Delete/{service.Id}");
	
		// Assert
		response.EnsureSuccessStatusCode();
		
		Service? deletedService = await GetEntityByIdAsync(service.Id);
		deletedService.Should().BeNull();
	
		await CleanEntitiesAsync();
	}
}