using System.Net.Http.Json;
using FluentAssertions;
using Flurl;
using SkillProfi.Application.CQRS.Posts.Queries.GetList;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.PostController;

public sealed class PostRetrievalTests(SkillProfiApplicationFactory<Program> factory) 
	: TestBase<Post>(factory)
{
	[Fact]
	public async Task GetPostList_Success()
	{
		// Arrange
		Post[] posts = 
		[
			TestPostData.Post1, 
			TestPostData.Post2, 
			TestPostData.Post3
		];

		await AddEntitiesAsync(posts);
		
		GetPostListQuery query = new()
		{
			PageNumber = 1,
			PageSize = 50
		};
		
		Url requestUri = "api/Post/GetList".SetQueryParams(query);

		try
		{
			//Act
			HttpResponseMessage response = await Client.GetAsync(requestUri);

			// Assert
			response.EnsureSuccessStatusCode();
			GetPostListResponse? postListResponse = await response.Content.ReadFromJsonAsync<GetPostListResponse>();
			postListResponse.Should().NotBeNull();
			postListResponse!.PageNumber.Should().Be(1);
			postListResponse.Count.Should().Be(posts.Length);
			
			for (int i = 0; i < postListResponse.Posts.Count; i++)
			{
				postListResponse.Posts[i].Title.Should().Be(posts[i].Title);
				postListResponse.Posts[i].Description.Should().Be(posts[i].Description);
			}
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}
