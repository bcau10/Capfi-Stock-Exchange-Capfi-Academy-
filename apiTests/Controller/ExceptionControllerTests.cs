using api.Controllers.v1;
using api.Utilities;
using System.Net;

namespace apiTests.Controller;

[TestFixture]
public class ExceptionControllerTests
{
    private ExceptionController exceptionController;

    [SetUp]
    public void Setup()
    {
        // Given
        exceptionController = new ExceptionController();
    }

    [Test]
    public void Error_ReturnsApiResponseWithStatusCode()
    {
        // Given
        int statusCodeValue = 404;
        var expectedApiResponse = new ApiResponse<string>((HttpStatusCode)statusCodeValue);

        // When
        var result = exceptionController.Error(statusCodeValue);

        // Then
        Assert.That(result.StatusCode, Is.EqualTo(expectedApiResponse.StatusCode));
    }
}