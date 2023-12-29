using api.Exceptions;

namespace apiTests.Exceptions;


[TestFixture]
public class CustomExceptionTests
{
    [Test]
    public void Constructor_GivenMessage_ShouldSetMessage()
    {
        // Given
        string message = "Test message";

        // When
        var customException = new CustomException(message);

        // Then
        Assert.That(customException.Message, Is.EqualTo(message));
        Assert.That(customException.InnerException, Is.Null);
    }

    [Test]
    public void Constructor_GivenMessageAndInnerException_ShouldSetMessageAndInnerException()
    {
        // Given
        string message = "Test message";
        var innerException = new Exception("Inner exception");

        // When
        var customException = new CustomException(message, innerException);

        // Then
        Assert.That(customException.Message, Is.EqualTo(message));
        Assert.That(customException.InnerException, Is.EqualTo(innerException));
    }
}