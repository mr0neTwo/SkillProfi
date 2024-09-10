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
public sealed class PostCreationTests(SkillProfiApplicationFactory<Program> factory) : TestBase<Post>(factory)
{
	[Fact]
	public async Task CreatePost_Success()
	{
		// Arrange
		Post testPost = TestPostData.Post1;
		
		CreatePostDto request = new()
		{
			Title = testPost.Title, 
			Description = testPost.Description
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/post/Create", request);

			// Assert
			response.EnsureSuccessStatusCode();
			int postId = await response.Content.ReadFromJsonAsync<int>();

			Post? post = await GetEntityByIdAsync(postId);
			post.Should().NotBeNull();
			post!.Title.Should().Be(request.Title);
			post.Description.Should().Be(request.Description);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task CreatePost_FailedByEmptyFields()
	{
		// Arrange
		CreatePostDto request = new();
		
		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/post/Create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			string[,] errors = 
			{
				{ "Title", "Title is required." },
				{ "Description", "Description is required." }
			};

			AssertErrors(jsonResponse, errors);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task CreatePost_FailedByOverLimitFields()
	{
		// Arrange
		CreatePostDto request = new()
		{
			Title = new string('x', FieldLimits.PostTitleMaxLength + 1),  
			Description = new string('x', FieldLimits.PostDescriptionMaxLength + 1)
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/post/Create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
			
			string[,] errors = 
			{
				{ "Title", $"Title must be at most {FieldLimits.PostTitleMaxLength} characters long." },
				{ "Description", $"Description must be at most {FieldLimits.PostDescriptionMaxLength} characters long." }
			};

			AssertErrors(jsonResponse, errors);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}