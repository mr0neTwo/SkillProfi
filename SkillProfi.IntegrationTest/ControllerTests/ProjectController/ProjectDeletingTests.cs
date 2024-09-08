using FluentAssertions;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.ProjectController;

[Collection(nameof(ApiTestCollection))]
public sealed class ProjectDeletingTests(SkillProfiApplicationFactory<Program> factory)
	: TestBase<Project>(factory)
{
	[Fact]
	public async Task DeleteProject_Success()
	{
		// Arrange
		Project project = TestProjectData.Project1;
		
		await AddEntitiesAsync(project);
		
		// Act
		HttpResponseMessage response = await Client.DeleteAsync($"api/Project/Delete/{project.Id}");
	
		// Assert
		response.EnsureSuccessStatusCode();
		
		Project? deletedProject = await GetEntityByIdAsync(project.Id);
		deletedProject.Should().BeNull();
	
		await CleanEntitiesAsync();
	}
}
