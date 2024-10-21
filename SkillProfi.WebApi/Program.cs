using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.IdentityModel.Tokens;
using SkillProfi.Application;
using SkillProfi.Application.Common.Mapping;
using SkillProfi.Application.Common.Settings;
using SkillProfi.Application.Interfaces;
using SkillProfi.Persistence;
using SkillProfi.WebApi.Middleware;
using SkillProfi.WebApi.Services.ImageService;

namespace SkillProfi.WebApi;

public class Program
{
	public static async Task Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
		
		string host = Environment.GetEnvironmentVariable("API_HOST")!;
		string port = Environment.GetEnvironmentVariable("API_PORT")!;
		builder.WebHost.UseUrls($"http://{host}:{port}");

		builder.Services.AddAutoMapper
		(
			config =>
			{
				config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
				config.AddProfile(new AssemblyMappingProfile(typeof(IAppContext).Assembly));
			}
		);
		
		string corsUrl = Environment.GetEnvironmentVariable("CORS_URL")!;

		builder.Services.AddCors
		(
			option =>
			{
				option.AddPolicy
				(
					"Policy", policy => policy.WithOrigins(corsUrl)
											  .AllowAnyHeader()
											  .AllowAnyMethod()
											  .AllowCredentials()
				);
			}
		);
		
		IConfigurationSection jwtSection = builder.Configuration.GetSection("JwtSettings");

		JwtSettings jwtSettings = new()
		{
			AccessTokenSecret = jwtSection["AccessTokenSecret"]!,
			AccessTokenExpirationHours = double.Parse(jwtSection["AccessTokenExpirationHours"]!),
			Issuer = jwtSection["Issuer"]!,
			Audience = jwtSection["Audience"]!,
			CookieFieldName = jwtSection["CookieFieldName"]!
		};
		
		builder.Services.AddSingleton(jwtSettings);
		
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			   .AddJwtBearer
			   (
				   options =>
				   {
					   options.TokenValidationParameters = new TokenValidationParameters
					   {
						   ValidIssuer = jwtSettings.Issuer,
						   ValidAudience = jwtSettings.Audience,
						   ValidateIssuer = true,
						   ValidateAudience = true,
						   ValidateLifetime = true,
						   ValidateIssuerSigningKey = true,
						   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessTokenSecret)),
					   };

					   options.Events = new JwtBearerEvents()
					   {
						   OnMessageReceived = context =>
						   {
							   context.Token = context.Request.Cookies[jwtSettings.CookieFieldName];
							   
							   return Task.CompletedTask;
						   }
					   };
				   }
			   );
		
		builder.Services.AddSwaggerGen
		(
			config =>
			{
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				config.IncludeXmlComments(xmlPath);
			}
		);
		
		builder.Services.AddAuthorization();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddApplication();
		builder.Services.AddPersistence(builder.Configuration);
		builder.Services.AddControllers();
		builder.Services.AddTransient<IImageStore, ImageStore>();

		var app = builder.Build();

		using (IServiceScope scope = app.Services.CreateScope())
		{
			IServiceProvider serviceProvider = scope.ServiceProvider;

			AppDbContext dbContext = (serviceProvider.GetRequiredService<IAppContext>() as AppDbContext)!;
			await DbInitializer.Initialize(dbContext);
		}
		
		app.UseSwagger();
		app.UseSwaggerUI();
		app.UseCustomExceptionHandler();
		app.UseRouting();
		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseCookiePolicy
		(
			new CookiePolicyOptions()
			{
				MinimumSameSitePolicy = SameSiteMode.Strict,
				HttpOnly = HttpOnlyPolicy.Always,
				Secure = CookieSecurePolicy.None
			}
		);

		app.UseCors("Policy");
		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();

		await app.RunAsync();
	}
}