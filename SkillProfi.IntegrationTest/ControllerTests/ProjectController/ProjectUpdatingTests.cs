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
public sealed class ProjectUpdatingTests(SkillProfiApplicationFactory<Program> factory)
	: TestBase<Project>(factory)
{
	[Fact]
	public async Task UpdateProject_Success()
	{
		// Arrange
		Project project = TestProjectData.Project1;

		await AddEntitiesAsync(project);
		
		UpdateProjectModel request = new()
		{
			Id = project.Id,
			Title = "Updated Title",
			Description = "Updated Description"
		};
		
		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/Project/Update", request);

			// Assert
			response.EnsureSuccessStatusCode();
			
			Project? updatedProject = await GetEntityByIdAsync(project.Id);

			updatedProject.Should().NotBeNull();
			updatedProject!.Title.Should().Be(request.Title);
			updatedProject.Description.Should().Be(request.Description);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
	
	[Fact]
	public async Task UpdateProject_FailedByOverLimitFields()
	{
		// Arrange
		Project project = TestProjectData.Project1;
		await AddEntitiesAsync(project);
		
		UpdateProjectModel request = new()
		{
			Id = project.Id,
			Title = new string('x', FieldLimits.ProjectTitleMaxLength + 1),  
			Description = new string('x', FieldLimits.ProjectDescriptionMaxLength + 1)
		};

		try
		{
			// Act
			HttpResponseMessage response = await Client.PutAsJsonAsync("api/Project/Update", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			string responseContent = await response.Content.ReadAsStringAsync();
			dynamic? jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

			string[,] errors = 
			{
				{ "Title", $"Title must be at least {FieldLimits.ProjectTitleMaxLength} characters long." },
				{ "Description", $"Description must be at least {FieldLimits.ProjectDescriptionMaxLength} characters long." }
			};

			AssertErrors(jsonResponse, errors);
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}
