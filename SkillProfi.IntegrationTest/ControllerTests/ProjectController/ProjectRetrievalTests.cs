using System.Net.Http.Json;
using FluentAssertions;
using Flurl;
using SkillProfi.Application.CQRS.Projects.Queries.GetList;
using SkillProfi.Domain;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests.ProjectController;

[Collection(nameof(ApiTestCollection))]
public sealed class ProjectRetrievalTests(SkillProfiApplicationFactory<Program> factory) 
	: TestBase<Project>(factory)
{
	[Fact]
	public async Task GetProjectList_Success()
	{
		// Arrange
		Project[] projects = 
		[
			TestProjectData.Project1, 
			TestProjectData.Project2, 
			TestProjectData.Project3
		];

		await AddEntitiesAsync(projects);
		
		GetProjectListQuery query = new()
		{
			PageNumber = 1,
			PageSize = 50
		};
		
		Url requestUri = "api/Project/GetList".SetQueryParams(query);

		try
		{
			//Act
			HttpResponseMessage response = await Client.GetAsync(requestUri);

			// Assert
			response.EnsureSuccessStatusCode();
			GetProjectListResponse? projectListResponse = await response.Content.ReadFromJsonAsync<GetProjectListResponse>();
			projectListResponse.Should().NotBeNull();
			projectListResponse!.PageNumber.Should().Be(1);
			projectListResponse.Count.Should().Be(projects.Length);
			
			for (int i = 0; i < projectListResponse.Projects.Count; i++)
			{
				projectListResponse.Projects[i].Title.Should().Be(projects[i].Title);
				projectListResponse.Projects[i].Description.Should().Be(projects[i].Description);
			}
		}
		finally
		{
			await CleanEntitiesAsync();
		}
	}
}
