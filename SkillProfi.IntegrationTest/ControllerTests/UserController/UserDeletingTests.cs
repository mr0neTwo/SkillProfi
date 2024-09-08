using FluentAssertions;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.UserController;

[Collection(nameof(ApiTestCollection))]
public sealed class UserDeletingTests(SkillProfiApplicationFactory<Program> factory) : TestBase<User>(factory)
{
	[Fact]
	public async Task DeleteUser_Success()
	{
		// Arrange
		User testUser = TestUserData.TestUser1;

		await AddEntitiesAsync(testUser);
		
	
		// Act
		HttpResponseMessage response = await Client.DeleteAsync($"api/user/delete/{testUser.Id}");
	
		// Assert
		response.EnsureSuccessStatusCode();
		
		User? updatedUser = await GetEntityByIdAsync(testUser.Id);
		updatedUser.Should().BeNull();
	
		await CleanEntitiesAsync();
	}
}
