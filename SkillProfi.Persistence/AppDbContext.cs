using Microsoft.EntityFrameworkCore;
using SkillProfi.Application.Interfaces;
using SkillProfi.Domain;
using SkillProfi.Persistence.EntityTypeConfigurations;

namespace SkillProfi.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<ClientRequest> ClientRequests { get; set; }
	public DbSet<Company> Companies { get; set; }
	public DbSet<Project> Projects { get; set; }
	public DbSet<Service> Services { get; set; }
	public DbSet<SiteItem> SiteItems { get; set; }
	public DbSet<SocialMedia> SocialMedias { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		
		modelBuilder.ApplyConfiguration(new UserConfiguration());
		modelBuilder.ApplyConfiguration(new ClientRequestConfiguration());
		modelBuilder.ApplyConfiguration(new CompanyConfiguration());
		modelBuilder.ApplyConfiguration(new ProjectConfiguration());
		modelBuilder.ApplyConfiguration(new ServiceConfiguration());
		modelBuilder.ApplyConfiguration(new SiteItemConfiguration());
		modelBuilder.ApplyConfiguration(new SocialMediaConfiguration());
	}
}
