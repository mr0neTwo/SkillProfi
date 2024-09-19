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
			Address = "Москва, ул. Пресненская набережная 8, стр. 1, оф. 212",
			DirectorName = "Атос де Ля Фер",
			MapLink = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3175.7653840786033!2d37.53812212411944!3d55.74798768758589!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x46b54bdd017303b9%3A0xd1f63f945a2450c2!2sMoscow%20City!5e0!3m2!1sen!2smk!4v1726556618515!5m2!1sen!2smk"
		};
	}
}
