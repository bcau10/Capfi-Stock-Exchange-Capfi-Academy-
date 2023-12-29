using api.Utilities;
using System.Net;

namespace apiTests.Utilities;

[TestFixture]
public class ApiResponseTests
{
    [Test]
    public void Constructor_WithStatusCode_SetsStatusCodeAndDefaultMessage()
    {
        // Given
        var statusCode = HttpStatusCode.BadRequest;

        // When
        var response = new ApiResponse<string>(statusCode);

        // Then
        Assert.Multiple(() =>
        {

            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.ErrorMessage, Is.EqualTo("A bad request, you have made"));
            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.Content, Is.Null);
        });
    }

    [Test]
    public void Constructor_WithStatusCodeAndMessage_SetsStatusCodeAndMessage()
    {
        // Then
        HttpStatusCode statusCode = HttpStatusCode.NotFound;
        string message = "Custom error message";

        // When
        var response = new ApiResponse<string>(statusCode, message);

        // Then
        Assert.Multiple(() =>
        {

            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.ErrorMessage, Is.EqualTo(message));
            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.Content, Is.Null);
        });
    }

    [Test]
    public void DefaultConstructor_SetsPropertiesToDefaultValues()
    {
        // Given When
        var response = new ApiResponse<string>();
        // Then
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ErrorMessage, Is.Null);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.Content, Is.Null);
        });
    }
}