using api.Dto;
using api.Utilities;
using core.Model;
using core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using core.Specifications.CustomSpecifications;

namespace api.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class OrderBooksController : ControllerBase
{
    private readonly IMapperBase _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderBooksController(IUnitOfWork unitOfWork, IMapperBase mapperBase)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapperBase;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<IEnumerable<OrderBookDto>>>> GetOrderBooks()
    {
        var result = await _unitOfWork.Repository<OrderBook>()
            .GetListWithSpecAsync(new OrderBookWithOrdersSpecification());

        return Ok(new ApiResponse<IEnumerable<OrderBookDto>>
        {
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Content = _mapper.MapList(result)
        });
    }
}