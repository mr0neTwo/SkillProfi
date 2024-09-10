using System.Net.Http.Json;
using FluentAssertions;
using SkillProfi.Application.CQRS.SocialMedia.Queries.GetList;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.SocialMediaController;

[Collection(nameof(ApiTestCollection))]
public sealed class SocialMediaRetrievalTests(SkillProfiApplicationFactory<Program> factory) 
	: TestBase<SocialMedia>(factory)
{
	[Fact]
	public async Task GetSocialMediaList_Success()
	{
		// Arrange
		SocialMedia[] socialMedia = 
		[
			TestSocialMediaData.SocialMedia1, 
			TestSocialMediaData.SocialMedia2, 
			TestSocialMediaData.SocialMedia3
		];

		await AddEntitiesAsync(socialMedia);
		
		try
		{
			//Act
			HttpResponseMessage response = await Client.GetAsync("api/SocialMedia/GetList");

			// Assert
			response.EnsureSuccessStatusCode();
			List<SocialMediaDto>? socialMediaDtos = await response.Content.ReadFromJsonAsync<List<SocialMediaDto>>();
			socialMediaDtos.Should().NotBeNull();
			
			for (int i = 0; i < socialMediaDtos!.Count; i++)
			{
				socialMediaDtos[i].IconName.Should().Be(socialMedia[i].IconName);
				socialMediaDtos[i].Link.Should().Be(socialMedia[i].Link);
			}
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}
