using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillProfi.Application.Interfaces;

namespace SkillProfi.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		string host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
		string port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
		string dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "skill_profi";
		string user = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
		string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "225567";

		string connectionString = $"Host={host};Port={port};Database={dbName};Username={user};Password={password}";

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
