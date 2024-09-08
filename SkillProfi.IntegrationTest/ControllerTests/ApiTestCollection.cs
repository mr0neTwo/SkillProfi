using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace SkillProfi.IntegrationTest.ControllerTests;

[CollectionDefinition(nameof(ApiTestCollection))]
public class ApiTestCollection : ICollectionFixture<SkillProfiApplicationFactory<Program>>
{
}
