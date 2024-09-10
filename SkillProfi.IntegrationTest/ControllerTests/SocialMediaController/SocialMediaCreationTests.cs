using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SkillProfi.Application.Common;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using SkillProfi.WebApi.Models.SocialMedia;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.SocialMediaController;

[Collection(nameof(ApiTestCollection))]
public sealed class SocialMediaCreationTests(SkillProfiApplicationFactory<Program> factory) : TestBase<SocialMedia>(factory)
{
	[Fact]
	public async Task CreateSocialMedia_Success()
	{
		// Arrange
		SocialMedia socialMedia = TestSocialMediaData.SocialMedia1;
		
		CreateSocialMediaDto request = new()
		{
			IconName = socialMedia.IconName, 
			Link = socialMedia.Link
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/SocialMedia/Create", request);

			// Assert
			response.EnsureSuccessStatusCode();
			int socialMediaId = await response.Content.ReadFromJsonAsync<int>();

			SocialMedia? createdSocialMedia = await GetEntityByIdAsync(socialMediaId);
			createdSocialMedia.Should().NotBeNull();
			createdSocialMedia!.IconName.Should().Be(request.IconName);
			createdSocialMedia.Link.Should().Be(request.Link);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task CreateSocialMedia_FailedByEmptyFields()
	{
		// Arrange
		CreateSocialMediaDto request = new();
		
		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/SocialMedia/Create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			string[,] errors = 
			{
				{ "IconName", "IconName is required." },
				{ "Link", "Link is required." }
			};

			AssertErrors(jsonResponse, errors);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task CreateSocialMedia_FailedByOverLimitFields()
	{
		// Arrange
		CreateSocialMediaDto request = new()
		{
			IconName = new string('x', FieldLimits.SocialMediaIconNameMaxLength + 1),  
			Link = new string('x', FieldLimits.SocialMediaLinkMaxLength + 1)
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/SocialMedia/Create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
			
			string[,] errors = 
			{
				{ "IconName", $"IconName must be at most {FieldLimits.SocialMediaIconNameMaxLength} characters long." },
				{ "Link", $"Link must be at most {FieldLimits.SocialMediaLinkMaxLength} characters long." }
			};

			AssertErrors(jsonResponse, errors);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}
