using System.Net;

namespace api.Utilities;

public class ApiResponse<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public T Content { get; set; }
    public string ErrorMessage { get; set; }

    public ApiResponse(HttpStatusCode statusCode, string errorMessage = null)
    {
        StatusCode = statusCode;
        ErrorMessage = errorMessage ?? GetDefaultMessageForStatus(StatusCode);
        IsSuccess = false;
        Content = default;
    }

    public ApiResponse()
    {
        StatusCode = HttpStatusCode.OK;
        IsSuccess = true;
        Content = default;
        ErrorMessage = null;
    }

    private static string GetDefaultMessageForStatus(HttpStatusCode statusCode) => statusCode switch
    {
        HttpStatusCode.BadRequest => "A bad request, you have made",
        HttpStatusCode.Unauthorized => "Authorized, you are not",
        HttpStatusCode.Forbidden => "Allowed in this area, you are not",
        HttpStatusCode.NotFound => "Resource found, it was not",
        HttpStatusCode.MethodNotAllowed => "Method allowed, it is not",
        HttpStatusCode.InternalServerError => "Errors are the path to the dark side. Erros lead to anger. Anger leads to hate" +
        " hate leads to career change",
        _ => string.Empty
    };
}