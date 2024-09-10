using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using SkillProfi.WebApi.Models.SiteItem;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.SiteItemController;

[Collection(nameof(ApiTestCollection))]
public sealed class SiteItemUpdatingTests(SkillProfiApplicationFactory<Program> factory)
	: TestBase<SiteItem>(factory)
{
	[Fact]
	public async Task UpdateSiteItem_Success()
	{
		// Arrange
		SiteItem siteItem = TestSiteItemData.SiteItem1;

		await AddEntitiesAsync(siteItem);
		
		UpdateSiteItemDto request = new()
		{
			Key = siteItem.Key,
			Title = "Updated Title"
		};
		
		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/SiteItem/Update", request);

			// Assert
			response.EnsureSuccessStatusCode();
			
			SiteItem? updatedSiteItem = await GetEntityByIdAsync(siteItem.Id);

			updatedSiteItem.Should().NotBeNull();
			updatedSiteItem!.Key.Should().Be(request.Key);
			updatedSiteItem.Title.Should().Be(request.Title);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task UpdateSiteItem_FailedByEmptyTitle()
	{
		// Arrange
		SiteItem siteItem = TestSiteItemData.SiteItem1;

		await AddEntitiesAsync(siteItem);

		UpdateSiteItemDto request = new()
		{
			Key = siteItem.Key
		};
	
		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/SiteItem/Update", request);
	
			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
	
			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
	
			((string)jsonResponse!.errors.Title[0]).Should().Be("The Title field is required.");
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}
