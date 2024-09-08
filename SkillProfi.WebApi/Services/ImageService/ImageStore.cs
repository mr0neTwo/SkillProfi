namespace SkillProfi.WebApi.Services.ImageService;

public sealed class ImageStore(IWebHostEnvironment environment) : IImageStore
{
	private const string FolderName = "images";

	public async Task<string> SaveImageAsync(string base64Image)
	{
		string fileName = $"{Guid.NewGuid()}.jpg";
		string savePath = Path.Combine(environment.WebRootPath, FolderName, fileName);

		if (!Directory.Exists(Path.Combine(environment.WebRootPath, FolderName)))
		{
			Directory.CreateDirectory(Path.Combine(environment.WebRootPath, FolderName));
		}

		string base64Data = base64Image.Substring(base64Image.IndexOf(',') + 1);

		byte[] imageBytes = Convert.FromBase64String(base64Data);

		await File.WriteAllBytesAsync(savePath, imageBytes);

		return $"{FolderName}/{fileName}";
	}
	
	public void DeleteImage(string imageUrl)
	{
		string root = environment.WebRootPath;
		string filePath = Path.Combine(root, imageUrl);

		if (!File.Exists(filePath))
		{
			return;
		}

		try
		{
			File.Delete(filePath);
		}
		catch (IOException ex)
		{
			Console.WriteLine($"Image deleting error: {ex.Message}");
		}
	}
}
