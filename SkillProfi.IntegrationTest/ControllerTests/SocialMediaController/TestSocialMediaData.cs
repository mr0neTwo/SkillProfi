using SkillProfi.Domain;

namespace SkillProfi.IntegrationTest.ControllerTests.SocialMediaController;

public static class TestSocialMediaData
{
	public static SocialMedia SocialMedia1 => new()
	{
		IconName = "FadeBook", 
		Link = "www.facebook.com"
	};
	
	public static SocialMedia SocialMedia2 => new()
	{
		IconName = "Instagram", 
		Link = "www.instagram.com"
	};
	
	public static SocialMedia SocialMedia3 => new()
	{
		IconName = "VK", 
		Link = "www.vk.ru"
	};
}
