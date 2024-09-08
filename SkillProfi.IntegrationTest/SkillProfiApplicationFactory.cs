using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SkillProfi.Persistence;

namespace SkillProfi.IntegrationTest;

public class SkillProfiApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureServices
		(
			async services => 
			{
				var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

				if (descriptor != null)
				{
					services.Remove(descriptor);
				}

				services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("InMemoryDbForTesting"); });
				
				services.AddAuthentication("Test")
						.AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

				var serviceProvider = services.BuildServiceProvider();

				using var scope = serviceProvider.CreateScope();

				IServiceProvider scopedServices = scope.ServiceProvider;
				AppDbContext db = scopedServices.GetRequiredService<AppDbContext>();
				
				await db.Database.EnsureCreatedAsync();
			}
		);
	}
}

public sealed class TestAuthHandler(
	IOptionsMonitor<AuthenticationSchemeOptions> options,
	ILoggerFactory logger,
	UrlEncoder encoder,
	ISystemClock clock) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)
{
	protected override Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		Claim[] claims = [new Claim(ClaimTypes.Name, "TestUser")];
		ClaimsIdentity identity = new(claims, "Test");
		ClaimsPrincipal principal = new(identity);
		AuthenticationTicket ticket = new(principal, "Test");

		return Task.FromResult(AuthenticateResult.Success(ticket));
	}
}