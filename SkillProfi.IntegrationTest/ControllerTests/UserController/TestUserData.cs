using SkillProfi.Domain;

namespace SkillProfi.IntegrationTest.ControllerTests.UserController;

public static class TestUserData
{
	public static User TestUser1 => new()
	{
		CreationDate = DateTime.Now,
		Name = "John Doe", 
		Email = "john.doe@example.com", 
		PasswordHash = "SecurePassword123!?"
	};
	
	public static User TestUser2 => new()
	{
		CreationDate = DateTime.Now,
		Name = "Jane Smith", 
		Email = "jane.smith@example.com", 
		PasswordHash = "SecurePassword123!?"
	};
	
	public static User TestUser3 => new()
	{
		CreationDate = DateTime.Now,
		Name = "Alice Johnson", 
		Email = "alice.johnson@example.com", 
		PasswordHash = "SecurePassword123!?"
	};
}
