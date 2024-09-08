namespace SkillProfi.WebApi.Services.ImageService;

public interface IImageStore
{
	public Task<string> SaveImageAsync(string base64Image);

	public void DeleteImage(string imageUrl);
}
