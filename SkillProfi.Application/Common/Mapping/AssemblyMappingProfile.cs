using System.Reflection;
using AutoMapper;

namespace SkillProfi.Application.Common.Mapping;

public sealed class AssemblyMappingProfile : Profile
{
	public AssemblyMappingProfile(Assembly assembly)
	{
		ApplyMappingsFromAssembly(assembly);
	}

	private void ApplyMappingsFromAssembly(Assembly assembly)
	{
		List<Type> types = assembly.GetExportedTypes()
								   .Where(HasIMapWithInterface)
								   .ToList();
		
		foreach (Type type in types)
		{
			object? instance = Activator.CreateInstance(type);
			MethodInfo? methodInfo = type.GetMethod("Mapping");
			methodInfo?.Invoke(instance, [this]);
		}
	}

	private static bool HasIMapWithInterface(Type type)
	{
		return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>));
	}
}