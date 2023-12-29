using api.Utilities;

namespace apiTests.Utilities;

[TestFixture]
public class ApiVersionTests
{
    [Test]
    public void ApiVersionProperties_InitializedCorrectly()
    {
        // Given
        var version = "1.0";
        var creatingTime = DateTime.UtcNow;

        // When
        var apiVersion = new ApiVersion
        (
            version,
            creatingTime
        );

        // Then
        Assert.Multiple(() =>
        {
            Assert.That(apiVersion.Version, Is.EqualTo(version));
            Assert.That(apiVersion.CreatingTime, Is.EqualTo(creatingTime));
        });
    }
}