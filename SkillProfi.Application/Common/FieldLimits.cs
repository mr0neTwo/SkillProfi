namespace SkillProfi.Application.Common;

public static class FieldLimits
{
	public const int UserNameMaxLength = 30;
	public const int UserEmailMaxLength = 30;
	public const int UserPasswordMaxLength = 50;
	public const int UserPasswordMinLength = 6;
	
	public const int ClientRequestNameMaxLength = 30;
	public const int ClientRequestEmailMaxLength = 30;
	public const int ClientRequestMessageMaxLength = 600;
	
	public const int SiteItemKexMaxLength = 10;
	public const int SiteItemTitleMaxLength = 100;
	
	public const int ServiceTitleMaxLength = 100;
	public const int ServiceDescriptionMaxLength = 4096;
	
	public const int ProjectTitleMaxLength = 100;
	public const int ProjectImageUrlMaxLength = 50;
	public const int ProjectDescriptionMaxLength = 4096;
	
	public const int MaxItemsPerPage = 50;
	
	
	public const long MinTimestamp = -62135596800;// 1 January 0001
	public const long MaxTimestamp = 253402300799;// 31 December 9999 года
}
