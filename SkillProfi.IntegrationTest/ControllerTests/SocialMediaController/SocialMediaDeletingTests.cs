using FluentAssertions;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.SocialMediaController;

[Collection(nameof(ApiTestCollection))]
public sealed class SocialMediaDeletingTests(SkillProfiApplicationFactory<Program> factory)
	: TestBase<SocialMedia>(factory)
{
	[Fact]
	public async Task DeleteSocialMedia_Success()
	{
		// Arrange
		SocialMedia socialMedia = TestSocialMediaData.SocialMedia1;
		
		await AddEntitiesAsync(socialMedia);
		
		// Act
		HttpResponseMessage response = await Client.DeleteAsync($"api/SocialMedia/Delete/{socialMedia.Id}");
	
		// Assert
		response.EnsureSuccessStatusCode();
		
		SocialMedia? deletedSocialMedia = await GetEntityByIdAsync(socialMedia.Id);
		deletedSocialMedia.Should().BeNull();
	
		await CleanEntitiesAsync();
	}
}
