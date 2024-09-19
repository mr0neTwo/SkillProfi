using SkillProfi.Domain;

namespace SkillProfi.IntegrationTest.ControllerTests.CompanyController;

public static class TestCompanyData
{
	public static Company Company => new Company()
	{
		Name = "Skill Profi",
		Email = "skillprofi@example.com",
		PhoneNumber = "123456789",
		Address = "Москва, ул. Московская 287, оф. 212",
		DirectorName = "Атос де Ля Фер",
		MapLink = "www.google.map.com"
	};
}
