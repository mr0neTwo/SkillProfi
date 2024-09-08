using Microsoft.EntityFrameworkCore;
using SkillProfi.Domain;

namespace SkillProfi.Application.Interfaces;

public interface IAppContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<ClientRequest> ClientRequests { get; set; }
	public DbSet<Company> Companies { get; set; }
	public DbSet<Project> Projects { get; set; }
	public DbSet<Service> Services { get; set; }
	public DbSet<SiteItem> SiteItems { get; set; }
	public DbSet<SocialMedia> SocialMedias { get; set; }
	
	public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
