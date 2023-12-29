using api.Dto;
using api.Utilities;
using core.Model;
using core.Repositories;
using core.Specifications.CustomSpecifications;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class PortfolioElementsController : ControllerBase
{
    private readonly IMapperBase _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioElementsController(IUnitOfWork unitOfWork, IMapperBase mapperBase)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapperBase;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<IEnumerable<PortfolioElementDto>>>> GetPortfolios()
    {
        var result = await _unitOfWork.Repository<PortfolioElement>().GetListAllAsync();

        return Ok(new ApiResponse<IEnumerable<PortfolioElementDto>>()
        {
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Content = _mapper.MapList(result)
        });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<PortfolioElementDto>>> GetPortfoliosById(int id)
    {
        var result = await _unitOfWork.Repository<PortfolioElement>().GetByIdAsync(id);

        return result == null
           ? NotFound(new ApiResponse<PortfolioElementDto>(HttpStatusCode.NotFound))
           : Ok(new ApiResponse<PortfolioElementDto>()
           {
               IsSuccess = true,
               StatusCode = HttpStatusCode.OK,
               Content = _mapper.Map(result)
           });

    }
}
