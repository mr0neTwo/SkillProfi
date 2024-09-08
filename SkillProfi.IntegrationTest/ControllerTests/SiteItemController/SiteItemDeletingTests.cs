using FluentAssertions;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.SiteItemController;

[Collection(nameof(ApiTestCollection))]
public sealed class SiteItemDeletingTests(SkillProfiApplicationFactory<Program> factory)
	: TestBase<SiteItem>(factory)
{
	[Fact]
	public async Task DeleteSiteItem_Success()
	{
		// Arrange
		SiteItem siteItem = TestSiteItemData.SiteItem1;
		
		await AddEntitiesAsync(siteItem);
		
	
		// Act
		HttpResponseMessage response = await Client.DeleteAsync($"api/SiteItem/Delete/{siteItem.Key}");
	
		// Assert
		response.EnsureSuccessStatusCode();
		
		SiteItem? deletedSiteItem = await GetEntityByIdAsync(siteItem.Id);
		deletedSiteItem.Should().BeNull();
	
		await CleanEntitiesAsync();
	}
}