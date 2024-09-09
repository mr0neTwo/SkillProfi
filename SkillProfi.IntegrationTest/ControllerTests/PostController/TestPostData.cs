using SkillProfi.Domain;

namespace SkillProfi.IntegrationTest.ControllerTests.PostController;

public class TestPostData
{
	public static Post Post1 => new()
	{
		Title = "Открытие нового офиса в Санкт-Петербурге",
		ImageUrl = "images/4.png",
		Description = "Мы рады сообщить об открытии нашего нового офиса в Санкт-Петербурге. Это расширение позволит нам быть ближе к нашим клиентам и предоставлять более оперативные IT-консалтинговые услуги."
	};

	public static Post Post2 => new()
	{
		Title = "Вебинар: Будущее кибербезопасности",
		ImageUrl = "images/5.png",
		Description = "Приглашаем вас принять участие в нашем вебинаре, посвящённом вопросам кибербезопасности. Наши эксперты обсудят текущие угрозы и тренды в защите данных для бизнеса."
	};

	public static Post Post3 => new()
	{
		Title = "Встреча с партнёрами: Обсуждение инновационных IT-решений",
		ImageUrl = "images/6.png",
		Description = "Недавно состоялась встреча с нашими ключевыми партнёрами, на которой мы обсудили последние инновации в IT-индустрии и стратегию совместного развития."
	};
}

