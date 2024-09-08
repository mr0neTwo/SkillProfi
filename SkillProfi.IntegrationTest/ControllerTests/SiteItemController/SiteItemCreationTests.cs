using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SkillProfi.Application.Common;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using SkillProfi.WebApi.Models.SiteItem;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.SiteItemController;

[Collection(nameof(ApiTestCollection))]
public sealed class SiteItemCreationTests(SkillProfiApplicationFactory<Program> factory) : TestBase<SiteItem>(factory)
{
	[Fact]
	public async Task CreateSiteItem_Success()
	{
		// Arrange
		CreateSiteItemModel request = new()
		{
			Key = TestSiteItemData.SiteItem1.Key, 
			Title = TestSiteItemData.SiteItem1.Title
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/SiteItem/Create", request);

			// Assert
			response.EnsureSuccessStatusCode();
			int siteItemId = await response.Content.ReadFromJsonAsync<int>();

			SiteItem? siteItem = await GetEntityByIdAsync(siteItemId);
			siteItem.Should().NotBeNull();
			siteItem!.Key.Should().Be(request.Key);
			siteItem.Title.Should().Be(request.Title);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task CreateSiteItem_FailedByEmptyFields()
	{
		// Arrange
		CreateSiteItemModel request = new();

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/SiteItem/Create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			((string)jsonResponse!.errors.Key[0]).Should().Be("The Key field is required.");
			((string)jsonResponse.errors.Title[0]).Should().Be("The Title field is required.");
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task CreateSiteItem_FailedByOverLimitFields()
	{
		// Arrange
		CreateSiteItemModel request = new()
		{
			Key = new string('x', FieldLimits.SiteItemKexMaxLength + 1), 
			Title = new string('x', FieldLimits.SiteItemTitleMaxLength + 1), 
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/SiteItem/Create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			((string)jsonResponse![0].PropertyName).Should().Be("Key");
			((string)jsonResponse[0].ErrorMessage).Should().Be($"Key must be at least {FieldLimits.SiteItemKexMaxLength} characters long.");
			((string)jsonResponse[1].PropertyName).Should().Be("Title");
			((string)jsonResponse[1].ErrorMessage).Should().Be($"Title must be at least {FieldLimits.SiteItemTitleMaxLength} characters long.");
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}