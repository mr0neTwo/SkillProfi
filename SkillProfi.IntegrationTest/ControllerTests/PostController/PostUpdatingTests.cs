using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SkillProfi.Application.Common;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using SkillProfi.WebApi.Models.Posts;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.PostController;

[Collection(nameof(ApiTestCollection))]
public sealed class PostUpdatingTests(SkillProfiApplicationFactory<Program> factory)
	: TestBase<Post>(factory)
{
	[Fact]
	public async Task UpdatePost_Success()
	{
		// Arrange
		Post post = TestPostData.Post1;

		await AddEntitiesAsync(post);
		
		UpdatePostModel request = new()
		{
			Id = post.Id,
			Title = "Updated Title",
			Description = "Updated Description"
		};
		
		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/Post/Update", request);

			// Assert
			response.EnsureSuccessStatusCode();
			
			Post? updatedPost = await GetEntityByIdAsync(post.Id);

			updatedPost.Should().NotBeNull();
			updatedPost!.Title.Should().Be(request.Title);
			updatedPost.Description.Should().Be(request.Description);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task UpdatePost_FailedByOverLimitFields()
	{
		// Arrange
		Post post = TestPostData.Post1;
		await AddEntitiesAsync(post);
		
		UpdatePostModel request = new()
		{
			Id = post.Id,
			Title = new string('x', FieldLimits.PostTitleMaxLength + 1),  
			Description = new string('x', FieldLimits.PostDescriptionMaxLength + 1)
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/Post/Update", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			string[,] errors = 
			{
				{ "Title", $"Title must be at least {FieldLimits.PostTitleMaxLength} characters long." },
				{ "Description", $"Description must be at least {FieldLimits.PostDescriptionMaxLength} characters long." }
			};

			AssertErrors(jsonResponse, errors);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}
