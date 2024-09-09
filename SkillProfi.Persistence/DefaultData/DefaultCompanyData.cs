using SkillProfi.Domain;

namespace SkillProfi.Persistence.DefaultData;

public static class DefaultCompanyData
{
	public static Company GetValue()
	{
		return new Company
		{
			Name = "Skill Profi",
			Email = "skillprofi@example.com",
			PhoneNumber = "123456789",
			Address = "Москва, ул. Московская 287, оф. 212",
			DirectorName = "Атос де Ля Фер"
		};
	}
}
