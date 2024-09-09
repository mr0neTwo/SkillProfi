using FluentAssertions;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.PostController;

[Collection(nameof(ApiTestCollection))]
public sealed class PostDeletingTests(SkillProfiApplicationFactory<Program> factory)
	: TestBase<Post>(factory)
{
	[Fact]
	public async Task DeletePost_Success()
	{
		// Arrange
		Post post = TestPostData.Post1;
		
		await AddEntitiesAsync(post);
		
		// Act
		HttpResponseMessage response = await Client.DeleteAsync($"api/Post/Delete/{post.Id}");
	
		// Assert
		response.EnsureSuccessStatusCode();
		
		Post? deletedPost = await GetEntityByIdAsync(post.Id);
		deletedPost.Should().BeNull();
	
		await CleanEntitiesAsync();
	}
}
