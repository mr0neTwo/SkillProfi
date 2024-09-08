using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillProfi.Application.Interfaces;

namespace SkillProfi.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");

		services.AddDbContext<AppDbContext>
		(
			options =>
			{
				options.UseNpgsql(connectionString);
			}
		);

		services.AddScoped<IAppContext>(provider => provider.GetService<AppDbContext>());

		return services;
	}
}
