using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SkillProfi.Application.Common.Behaviors;
using SkillProfi.Application.Services.AuthService;
using SkillProfi.Application.Services.TokenService;

namespace SkillProfi.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(executingAssembly));
		services.AddValidatorsFromAssemblies(new[] { executingAssembly });
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
		services.AddScoped<ITokenProvider, TokenProvider>();
		services.AddScoped<IAuthService, AuthService>();
		
		return services;
	}
}
