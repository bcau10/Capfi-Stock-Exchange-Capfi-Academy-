using api.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class HealthCheckController : ControllerBase
{
    [HttpGet("version")]
    [ProducesResponseType(typeof(Utilities.ApiVersion), StatusCodes.Status200OK)]
    public ActionResult<ApiResponse<Utilities.ApiVersion>> GetVersion()
        => Ok(new ApiResponse<Utilities.ApiVersion>
        {
            Content = new Utilities.ApiVersion("0.0.3", DateTime.Now),
            StatusCode = HttpStatusCode.OK
        });
}