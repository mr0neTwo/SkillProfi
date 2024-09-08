using SkillProfi.Domain;

namespace SkillProfi.IntegrationTest.ControllerTests.ClientRequestController;

public static class TestClientRequestData
{
	public static ClientRequest ClientRequest1 =>
		new()
		{
			CreationDate = new DateTime(2024, 8, 15, 0, 0, 0, DateTimeKind.Utc),
			ClientName = "John Doe",
			ClientEmail = "johndoe@example.com",
			Message = "I would like to inquire about your services.",
			Status = ClientRequestStatus.Received
		};
	
	public static ClientRequest ClientRequest2 =>
		new()
		{
			CreationDate = new DateTime(2024, 8, 16, 0, 0, 0, DateTimeKind.Utc),
			ClientName = "Jane Smith",
			ClientEmail = "janesmith@example.com",
			Message = "Could you provide more information on pricing?",
			Status = ClientRequestStatus.Received
		};
	
	public static ClientRequest ClientRequest3 =>
		new()
		{
			CreationDate = new DateTime(2024, 8, 17, 0, 0, 0, DateTimeKind.Utc),
			ClientName = "Michael Brown",
			ClientEmail = "michaelbrown@example.com",
			Message = "I am interested in setting up a meeting.",
			Status = ClientRequestStatus.Received
		};
}
