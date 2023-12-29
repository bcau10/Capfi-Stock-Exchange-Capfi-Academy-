using api.Dto;
using api.Utilities;
using core.Model;
using core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AccountsController : ControllerBase
{
    private readonly IMapperBase _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AccountsController(IUnitOfWork unitOfWork, IMapperBase mapperBase)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapperBase;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<IEnumerable<CustomerDto>>>> GetAccounts()
    {
        var result = await _unitOfWork.Repository<Customer>().GetListAllAsync();

        return Ok(new ApiResponse<IEnumerable<CustomerDto>>()
        {
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Content = _mapper.MapList(result)
        });
    }
}
