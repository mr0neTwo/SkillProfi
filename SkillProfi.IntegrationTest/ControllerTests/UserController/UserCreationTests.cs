using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using SkillProfi.WebApi.Models.Users;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.UserController;

[Collection(nameof(ApiTestCollection))]
public sealed class UserCreationTests(SkillProfiApplicationFactory<Program> factory) : TestBase<User>(factory)
{
	[Fact]
	public async Task CreateUser_Success()
	{
		// Arrange
		CreateUserDto request = new()
		{
			Name = TestUserData.TestUser1.Name, 
			Email = TestUserData.TestUser1.Email, 
			Password = TestUserData.TestUser1.PasswordHash
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/user/Create", request);

			// Assert
			response.EnsureSuccessStatusCode();
			int userId = await response.Content.ReadFromJsonAsync<int>();

			User? user = await GetEntityByIdAsync(userId);
			user.Should().NotBeNull();
			user!.Name.Should().Be(request.Name);
			user.Email.Should().Be(request.Email);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task CreateUser_FailedByNotUniqueEmail()
	{
		// Arrange
		User testUser = TestUserData.TestUser1;
		await AddEntitiesAsync(testUser);
		
		CreateUserDto request = new()
		{
			Name = testUser.Name, 
			Email = testUser.Email, 
			Password = testUser.PasswordHash
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/user/Create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			((string)jsonResponse![0].PropertyName).Should().Be("Email");
			((string)jsonResponse[0].ErrorMessage).Should().Be("Email already exists.");
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task CreateUser_FailedByIncorrectPassword()
	{
		// Arrange
		CreateUserDto request = new()
		{
			Name = TestUserData.TestUser1.Name, 
			Email = TestUserData.TestUser1.Email, 
			Password = "123456"
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/user/Create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			((string)jsonResponse![0].PropertyName).Should().Be("Password");
			((string)jsonResponse[0].ErrorMessage).Should().Be("Password must contain at least one uppercase letter.");
			((string)jsonResponse[1].ErrorMessage).Should().Be("Password must contain at least one lowercase letter.");
			((string)jsonResponse[2].ErrorMessage).Should().Be("Password must contain at least one special character (!?*.).");
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}