using SkillProfi.Domain;

namespace SkillProfi.Persistence;

public static class DefaultSiteItems
{
	public static SiteItem[] GetValues()
	{
		SiteItem[] siteItems =
		[
			new SiteItem { Key = "Messages", Title = "Обращения" },
			new SiteItem { Key = "Main", Title = "Главная" },
			new SiteItem { Key = "Services", Title = "Услуги" },
			new SiteItem { Key = "Projects", Title = "Проекты" },
			new SiteItem { Key = "Blog", Title = "Блог" },
			new SiteItem { Key = "Contacts", Title = "Контакты" },
			new SiteItem { Key = "Users", Title = "Пользователи" },
			new SiteItem { Key = "Login", Title = "Войти" },
			new SiteItem { Key = "Logout", Title = "Выйти" },
			new SiteItem { Key = "ToSite", Title = "На сайт" },
			new SiteItem { Key = "Edit", Title = "Редактировать" },
			new SiteItem { Key = "TitleQuote", Title = "\"Расширяем возможности\"" },
			new SiteItem { Key = "MainQuote", Title = "IT консалтинг без регистрации и смс" },
		];
		
		return siteItems;
	}
}
