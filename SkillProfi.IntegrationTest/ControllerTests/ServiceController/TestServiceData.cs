using SkillProfi.Domain;

namespace SkillProfi.IntegrationTest.ControllerTests.ServiceController;

public static class TestServiceData
{
	public static Service TestService1 => new()
	{
		Title = "Web Development",
		Description = "Comprehensive web development services including frontend and backend."
	};
    
	public static Service TestService2 => new()
	{
		Title = "Mobile App Development",
		Description = "Development of cross-platform mobile applications for Android and iOS."
	};
    
	public static Service TestService3 => new()
	{
		Title = "Cloud Computing",
		Description = "Services related to cloud infrastructure, storage, and computing solutions."
	};
}