using System.Net.Http.Json;
using FluentAssertions;
using SkillProfi.Application.CQRS.SiteItems.Queries.Get;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.SiteItemController;

[Collection(nameof(ApiTestCollection))]
public sealed class SiteItemRetrievalTests(SkillProfiApplicationFactory<Program> factory) 
	: TestBase<SiteItem>(factory)
{
	[Fact]
	public async Task GetSiteItem_Success()
	{
		// Arrange
		SiteItem siteItem = TestSiteItemData.SiteItem1;
		await AddEntitiesAsync(siteItem);
		
		try
		{
			//Act
			HttpResponseMessage response = await Client.GetAsync($"api/SiteItem/Get/{siteItem.Key}");

			// Assert
			response.EnsureSuccessStatusCode();
			SiteItemDto? siteItemDto = await response.Content.ReadFromJsonAsync<SiteItemDto>();
			siteItemDto.Should().NotBeNull();
			siteItemDto!.Key.Should().Be(siteItem.Key);
			siteItemDto.Title.Should().Be(siteItem.Title);
		}
		finally
		{
			await CleanEntitiesAsync();
		} 
	}
}