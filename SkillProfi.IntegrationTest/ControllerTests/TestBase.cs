using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SkillProfi.Domain;
using SkillProfi.Persistence;
using SkillProfi.WebApi;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests;

public abstract class TestBase<TEntity> : IClassFixture<SkillProfiApplicationFactory<Program>>
	where TEntity : Entity
{
	private readonly SkillProfiApplicationFactory<Program> _factory;
	protected readonly HttpClient Client;

	protected TestBase(SkillProfiApplicationFactory<Program> factory)
	{
		_factory = factory;
		Client = factory.CreateClient();
		
		using var serviceScope = _factory.Services.CreateScope();
		var appContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
		appContext.Set<TEntity>().RemoveRange(appContext.Set<TEntity>());
		appContext.SaveChanges();
	}
	
	protected async Task CleanEntitiesAsync()
	{
		using var serviceScope = _factory.Services.CreateScope();
		var appContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

		var entities = await appContext.Set<TEntity>().ToListAsync();

		if (entities.Count == 0)
		{
			return;
		}
		
		appContext.Set<TEntity>().RemoveRange(entities);
		await appContext.SaveChangesAsync();
	}

	protected async Task AddEntitiesAsync(params TEntity[] entities)
	{
		using var serviceScope = _factory.Services.CreateScope();
		var appContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

		await appContext.Set<TEntity>().AddRangeAsync(entities);
		await appContext.SaveChangesAsync();
	}

	protected async Task<TEntity?> GetEntityByIdAsync(int entityId)
	{
		using var serviceScope = _factory.Services.CreateScope();
		var appContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

		return await appContext.Set<TEntity>().FindAsync(entityId);
	}

	protected void AssertErrors(dynamic jsonResponse, string[,] errors)
	{
		for (int i = 0; i < errors.GetLength(0); i++)
		{
			string expectedPropertyName = errors[i, 0];
			string expectedErrorMessage = errors[i, 1];

			((string)jsonResponse[i].PropertyName).Should().Be(expectedPropertyName);
			((string)jsonResponse[i].ErrorMessage).Should().Be(expectedErrorMessage);
		}
	}
}
