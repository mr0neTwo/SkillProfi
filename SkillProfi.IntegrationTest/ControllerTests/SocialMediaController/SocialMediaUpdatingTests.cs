using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SkillProfi.Application.Common;
using SkillProfi.Application.CQRS.SocialMedia.Queries.GetList;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using SkillProfi.WebApi.Models.SocialMedia;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.SocialMediaController;

[Collection(nameof(ApiTestCollection))]
public sealed class SocialMediaUpdatingTests(SkillProfiApplicationFactory<Program> factory)
	: TestBase<SocialMedia>(factory)
{
	[Fact]
	public async Task UpdateSocialMedia_Success()
	{
		// Arrange
		SocialMedia socialMedia = TestSocialMediaData.SocialMedia1;

		await AddEntitiesAsync(socialMedia);
		
		UpdateSocialMediaDto request = new()
		{
			Id = socialMedia.Id,
			IconName = "Updated IconName",
			Link = "Updated Link"
		};
		
		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/SocialMedia/Update", request);

			// Assert
			response.EnsureSuccessStatusCode();
			
			SocialMedia? updatedSocialMedia = await GetEntityByIdAsync(socialMedia.Id);

			updatedSocialMedia.Should().NotBeNull();
			updatedSocialMedia!.Link.Should().Be(request.Link);
			updatedSocialMedia.IconName.Should().Be(request.IconName);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task UpdateSocialMedia_FailedByOverLimitFields()
	{
		// Arrange
		SocialMedia socialMedia = TestSocialMediaData.SocialMedia1;
		await AddEntitiesAsync(socialMedia);
		
		UpdateSocialMediaDto request = new()
		{
			Id = socialMedia.Id,
			IconName = new string('x', FieldLimits.SocialMediaIconNameMaxLength + 1),  
			Link = new string('x', FieldLimits.SocialMediaLinkMaxLength + 1)
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/SocialMedia/Update", request);

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
	
	[Fact]
	public async Task UpdateAllSocialMedia_Success()
	{
		// Arrange
		SocialMedia socialMedia = TestSocialMediaData.SocialMedia1;

		List<SocialMediaDto> request = 
		[
			new SocialMediaDto()
			{
				IconName = TestSocialMediaData.SocialMedia2.IconName,
				Link = TestSocialMediaData.SocialMedia2.Link
			},
			new SocialMediaDto()
			{
				IconName = TestSocialMediaData.SocialMedia3.IconName,
				Link = TestSocialMediaData.SocialMedia3.Link
			}
		];

		await AddEntitiesAsync(socialMedia);

		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/SocialMedia/UpdateAll", request);
			request.Reverse();
			
			// Assert
			response.EnsureSuccessStatusCode();
			
			List<SocialMedia> updatedSocialMedia = await GetAllEntitiesAsync();

			updatedSocialMedia.Should().NotBeNull();
			updatedSocialMedia.Count.Should().Be(request.Count);

			for (int i = 0; i < updatedSocialMedia.Count; i++)
			{
				updatedSocialMedia[i].Link.Should().Be(request[i].Link);
				updatedSocialMedia[i].IconName.Should().Be(request[i].IconName);
			}
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}