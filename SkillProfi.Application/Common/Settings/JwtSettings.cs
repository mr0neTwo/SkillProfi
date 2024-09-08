namespace SkillProfi.Application.Common.Settings;

public sealed class JwtSettings
{
	public string AccessTokenSecret { get; set; }
	public double AccessTokenExpirationHours { get; set; }
	public string Issuer { get; set; }
	public string Audience { get; set; }
	public string CookieFieldName { get; set; }
}
