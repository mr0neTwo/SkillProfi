using SkillProfi.Domain;

namespace SkillProfi.IntegrationTest.ControllerTests.SiteItemController;

public static class TestSiteItemData
{
	public static SiteItem SiteItem1 => new() { Key = "Messages", Title = "Обращения" };
	public static SiteItem SiteItem2 => new() { Key = "Main", Title = "Главная" };
	public static SiteItem SiteItem3 => new() { Key = "Services", Title = "Услуги" };
}
