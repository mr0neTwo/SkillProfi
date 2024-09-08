namespace SkillProfi.Persistence;

public static class DbInitializer
{
	public static async Task Initialize(AppDbContext dbContext)
	{
		await dbContext.Database.EnsureDeletedAsync();
		await dbContext.Database.EnsureCreatedAsync();

		await FillDefaultSiteItems(dbContext);
		await FillDefaultValues(dbContext);
	}

	private static async Task FillDefaultSiteItems(AppDbContext dbContext)
	{
		await dbContext.SiteItems.AddRangeAsync(DefaultSiteItems.GetValues());
		await dbContext.SaveChangesAsync();
	}

	private static async Task FillDefaultValues(AppDbContext dbContext)
	{
		await dbContext.Users.AddRangeAsync(DefaultData.Users());
		await dbContext.ClientRequests.AddRangeAsync(DefaultData.ClientRequests());
		await dbContext.Services.AddRangeAsync(DefaultData.Services());
		await dbContext.Projects.AddRangeAsync(DefaultData.Projects());
		
		await dbContext.SaveChangesAsync();
	}
}
