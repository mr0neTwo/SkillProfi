using SkillProfi.Persistence.DefaultData;

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
		await dbContext.Companies.AddAsync(DefaultCompanyData.GetValue());
		await dbContext.SaveChangesAsync();
	}

	private static async Task FillDefaultValues(AppDbContext dbContext)
	{
		await dbContext.Users.AddRangeAsync(DefaultContent.Users());
		await dbContext.ClientRequests.AddRangeAsync(DefaultContent.ClientRequests());
		await dbContext.Services.AddRangeAsync(DefaultContent.Services());
		await dbContext.Projects.AddRangeAsync(DefaultContent.Projects());
		await dbContext.Posts.AddRangeAsync(DefaultContent.Posts());
		await dbContext.SocialMedias.AddRangeAsync(DefaultContent.SocialMedias());
		
		await dbContext.SaveChangesAsync();
	}
}
