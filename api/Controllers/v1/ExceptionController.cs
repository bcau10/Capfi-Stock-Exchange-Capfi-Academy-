using api.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api.Controllers.v1;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("error/{code}")]
[ApiVersion("1.0")]
public class ExceptionController
{
    public ApiResponse<string> Error(int code) => new()
    {
        StatusCode = (HttpStatusCode)code
    };
}
