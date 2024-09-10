using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using SkillProfi.Application.CQRS.ClientRequests.Queries.Get;
using SkillProfi.Application.Services.AuthService;
using SkillProfi.WebApi.Models.Auth;

namespace SkillProfi.WfpClient.Modules.Main;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private readonly HttpClient _httpClient;
	
	public MainWindow()
	{
		InitializeComponent();
		_httpClient = new HttpClient();
	}

	private async void Login_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			var loginModel = new UserLoginDto
			{
				Email = "admin@example.com",
				Password = "123456"
			};
			
			string jsonContent = JsonSerializer.Serialize(loginModel);
			var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
			
			HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5272/api/Auth/Login", content);
			response.EnsureSuccessStatusCode();
			
			string responseBody = await response.Content.ReadAsStringAsync();
			AuthResponse? authResponse = JsonSerializer.Deserialize<AuthResponse>(responseBody);

			if (authResponse != null)
			{
				Console.WriteLine($"User: {authResponse.User}");
				Console.WriteLine($"Token: {authResponse.Token}");
				Console.WriteLine($"Success: {authResponse.Success}");
				Console.WriteLine($"ErrorMessage: {authResponse.ErrorMessage}");
			}
			else
			{
				Console.WriteLine("Response is null or could not be deserialized.");
			}
		}
		catch (Exception exception)
		{
			Console.WriteLine(exception);
		}
	}

	private async void LoadMessages_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			long startTimestamp = new DateTimeOffset(new DateTime(2024, 8, 1, 0, 0, 0, DateTimeKind.Utc)).ToUnixTimeSeconds();
			long endTimestamp = new DateTimeOffset(new DateTime(2024, 8, 30, 0, 0, 0, DateTimeKind.Utc)).ToUnixTimeSeconds();
			
			string requestUri = $"http://localhost:5272/api/ClientRequest/GetList?start={startTimestamp}&end={endTimestamp}";
			HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
			response.EnsureSuccessStatusCode();
			
			string responseBody = await response.Content.ReadAsStringAsync();
			var authResponse = JsonSerializer.Deserialize<List<ClientRequestDto>>(responseBody);

			if (authResponse != null)
			{
				foreach (ClientRequestDto requestDto in authResponse)
				{
					Console.WriteLine($"Id: {requestDto.Id}, ClientName: {requestDto.ClientName}, ClientEmail: {requestDto.ClientEmail}");
				}
			}
			else
			{
				Console.WriteLine("Response is null or could not be deserialized.");
			}
		}
		catch (Exception exception)
		{
			Console.WriteLine(exception);
		}
	}

	
}
