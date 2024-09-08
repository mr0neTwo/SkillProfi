using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using SkillProfi.WebApi.Models;
using SkillProfi.WebApi.Models.Users;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.UserController;

[Collection(nameof(ApiTestCollection))]
public sealed class UserUpdatingTests(SkillProfiApplicationFactory<Program> factory) : TestBase<User>(factory)
{
	[Fact]
	public async Task UpdateUser_Success()
	{
		// Arrange
		User testUser = TestUserData.TestUser1;
		await AddEntitiesAsync(testUser);
		
		UpdateUserDto request = new()
		{
			Id = testUser.Id,
			Name = "Neo updated", 
			Email = "updated@example.com"
		};
		
		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/user/update", request);

			// Assert
			response.EnsureSuccessStatusCode();
			
			User? updatedUser = await GetEntityByIdAsync(testUser.Id);

			updatedUser.Should().NotBeNull();
			updatedUser!.Name.Should().Be(request.Name);
			updatedUser.Email.Should().Be(request.Email);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}

	[Fact]
	public async Task UpdateUser_FailedByNotUniqueEmail()
	{
		// Arrange
		User testUser1 = TestUserData.TestUser1;
		User testUser2 = TestUserData.TestUser2;

		await AddEntitiesAsync(testUser1, testUser2);
	
		UpdateUserDto request = new()
		{
			Id = testUser1.Id, 
			Email = testUser2.Email,
		};
	
		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/user/Update", request);
	
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
	public async Task UpdateUser_FailedByIncorrectPassword()
	{
		// Arrange
		User testUser = TestUserData.TestUser1;
		await AddEntitiesAsync(testUser);
	
		UpdateUserDto request = new()
		{
			Id = testUser.Id, 
			Password = "123456"
		};
	
		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/user/Update", request);
	
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