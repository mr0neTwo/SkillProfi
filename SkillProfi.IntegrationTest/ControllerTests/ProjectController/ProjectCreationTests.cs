using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SkillProfi.Application.Common;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using SkillProfi.WebApi.Models.Projects;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.ProjectController;

[Collection(nameof(ApiTestCollection))]
public sealed class ProjectCreationTests(SkillProfiApplicationFactory<Program> factory) : TestBase<Project>(factory)
{
	[Fact]
	public async Task CreateProject_Success()
	{
		// Arrange
		Project testProject = TestProjectData.Project1;
		
		CreateProjectDto request = new()
		{
			Title = testProject.Title, 
			Description = testProject.Description
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/project/Create", request);

			// Assert
			response.EnsureSuccessStatusCode();
			int projectId = await response.Content.ReadFromJsonAsync<int>();

			Project? project = await GetEntityByIdAsync(projectId);
			project.Should().NotBeNull();
			project!.Title.Should().Be(request.Title);
			project.Description.Should().Be(request.Description);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task CreateProject_FailedByEmptyFields()
	{
		// Arrange
		CreateProjectDto request = new();
		
		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/project/Create", request);

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
	public async Task CreateProject_FailedByOverLimitFields()
	{
		// Arrange
		CreateProjectDto request = new()
		{
			Title = new string('x', FieldLimits.ProjectTitleMaxLength + 1),  
			Description = new string('x', FieldLimits.ProjectDescriptionMaxLength + 1)
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PostAsJsonAsync("api/project/Create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
			
			string[,] errors = 
			{
				{ "Title", $"Title must be at most {FieldLimits.ProjectTitleMaxLength} characters long." },
				{ "Description", $"Description must be at most {FieldLimits.ProjectDescriptionMaxLength} characters long." }
			};

			AssertErrors(jsonResponse, errors);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}