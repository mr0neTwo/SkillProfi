using System.Net.Http.Json;
using FluentAssertions;
using SkillProfi.Application.CQRS.Users.Queries.Get;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.UserController;

[Collection(nameof(ApiTestCollection))]
public sealed class UserRetrievalTests(SkillProfiApplicationFactory<Program> factory) : TestBase<User>(factory)
{
	[Fact]
	public async Task GetUserList_Success()
	{
		// Arrange
		User[] users = [TestUserData.TestUser1, TestUserData.TestUser2, TestUserData.TestUser3];

		await AddEntitiesAsync(users);
		
		try
		{
			//Act
			HttpResponseMessage response = await Client.GetAsync("api/User/GetList");

			// Assert
			response.EnsureSuccessStatusCode();
			List<UserDto>? userDtos = await response.Content.ReadFromJsonAsync<List<UserDto>>();
			userDtos.Should().NotBeNull();
			userDtos!.Count.Should().Be(users.Length);

			for (int i = 0; i < userDtos.Count; i++)
			{
				userDtos[i].Name.Should().Be(users[i].Name);
				userDtos[i].Email.Should().Be(users[i].Email);
			}
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task GetUser_Success()
	{
		// Arrange
		User testUser = TestUserData.TestUser1;

		await AddEntitiesAsync(testUser);
		
		try
		{
			//Act
			HttpResponseMessage response = await Client.GetAsync($"api/User/Get/{testUser.Id}");

			// Assert
			response.EnsureSuccessStatusCode();
			UserDto? userDto = await response.Content.ReadFromJsonAsync<UserDto>();
			userDto.Should().NotBeNull();
			userDto!.Name.Should().Be(testUser.Name);
			userDto.Email.Should().Be(testUser.Email);
		}
		finally
		{
			await CleanEntitiesAsync();
		} 
	}
}