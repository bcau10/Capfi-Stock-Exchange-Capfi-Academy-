using System.ComponentModel.DataAnnotations;
using api.Utilities;
using core.Model;
using core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using core.Specifications.CustomSpecifications;
using core.Specifications.SpecificationParams;
using api.Dto;
using core.Services;
using core.Specifications;
using core.Utils;

namespace api.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class StockActionsController : ControllerBase
{
    private readonly IMapperBase _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IComputationService _computationService;

    public StockActionsController(IUnitOfWork unitOfWork, IMapperBase mapperBase,
        IComputationService computationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapperBase;
        _computationService = computationService;
    }

    [HttpGet]
    [ResponseCache(CacheProfileName = "Default120")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<IEnumerable<StockActionDto>>>> GetActionsAsync()
    {
        var result = await _unitOfWork.Repository<StockAction>().GetListAllAsync();

        return Ok(
            new ApiResponse<IEnumerable<StockActionDto>>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Content = _mapper.MapList(result)
            });
    }

    [HttpGet("{id:int}")]
    [ResponseCache(CacheProfileName = "Default120")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<StockActionDto>>> GetActionByIdAsync([Required] int id)
    {
        var result = await _unitOfWork.Repository<StockAction>().GetByIdAsync(id);

        return result == null
            ? NotFound(new ApiResponse<StockActionDto>(HttpStatusCode.NotFound))
            : Ok(new ApiResponse<StockActionDto>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Content = _mapper.Map(result)
            });
    }

    [HttpGet("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<StockActionDto>>> GetActionWithSpecifications(
        [Required] [FromQuery] StockActionSpecificationParams stockActionParams)
    {
        var result = await _unitOfWork.Repository<StockAction>()
            .GetEntityWithSpec(new StockActionWithPagingAndFilteringSpecification(stockActionParams));

        return result == null
            ? NotFound(new ApiResponse<StockActionDto>(HttpStatusCode.NotFound))
            : Ok(new ApiResponse<StockActionDto>()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Content = _mapper.Map(result)
            });
    }

    [HttpGet("filterAll")]
    [ProducesResponseType(StatusCodes.Status206PartialContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<Pagination<StockActionDto>>>> GetActionsWithSpecifications(
        [Required] [FromQuery] StockActionSpecificationParams stockActionParams)
    {
        var result = await _unitOfWork.Repository<StockAction>()
            .GetListWithSpecAsync(new StockActionWithPagingAndFilteringSpecification(stockActionParams));

        var count = await _unitOfWork.Repository<StockAction>()
            .CountAsync(new StockActionWithFilteringSpecification(stockActionParams));

        return result == null
            ? NotFound(new ApiResponse<Pagination<StockActionDto>>(HttpStatusCode.NotFound))
            : StatusCode((int)HttpStatusCode.PartialContent, new ApiResponse<Pagination<StockActionDto>>()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.PartialContent,
                Content = new(stockActionParams.PageIndex, stockActionParams.PageSize, count, _mapper.MapList(result))
            });
    }

    [HttpPost("buy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<PortfolioElementDto>>> BuyAction(
        [FromBody] OrderStockActionDto buyActionDto)
    {
        var customer = await _unitOfWork.Repository<Customer>()
            .GetEntityWithSpec(new CustomerWithPortfolioElementSpecification(buyActionDto.ClientId));

        if (customer == null)
            return BadRequest(new ApiResponse<IEnumerable<PortfolioElementDto>>
                (HttpStatusCode.BadRequest, $"Found, the Customer {buyActionDto.ClientId} was not"));

        var action = await _unitOfWork.Repository<StockAction>()
            .GetEntityWithSpec(new StockActionWithSymbolSpecification(buyActionDto.Symbol));

        if (action == null)
            return BadRequest(new ApiResponse<IEnumerable<PortfolioElementDto>>
                (HttpStatusCode.BadRequest, $"Found, the action {buyActionDto.Symbol} was not"));

        var forexRate = await _unitOfWork.Repository<ForexRate>()
            .GetEntityWithSpec(new ForexRateWithCurrencySpecification(action.TradingCurrency));

        if (forexRate == null)
            return BadRequest(new ApiResponse<IEnumerable<PortfolioElementDto>>
                (HttpStatusCode.BadRequest, $"Found, the Forex of {action.TradingCurrency} was not"));

        var orderBook = await _unitOfWork.Repository<OrderBook>()
            .GetEntityWithSpec(new OrderBookByIdWithOrdersSpecification(action.Id));

        if (orderBook == null)
            return BadRequest(new ApiResponse<IEnumerable<PortfolioElementDto>>
                (HttpStatusCode.BadRequest, $"Found, the OrderBook of {buyActionDto.Symbol} was not"));
        
        var orderCharacteristics = _computationService.OrderBookPrice
            (orderBook, customer.Id, buyActionDto.Quantity, OrderType.Sell);

        foreach (var orderBookElement in orderBook.OrderElements)
        {
            if(orderBookElement.Quantity == 0)
                _unitOfWork.Repository<Order>().Delete(orderBookElement);
            else
                _unitOfWork.Repository<Order>().Update(orderBookElement);
        }

        action.MarketPrice = orderCharacteristics.Price;
        _unitOfWork.Repository<StockAction>().Update(action);

        var priceOrder = _computationService.PriceToEur(orderCharacteristics.Price, forexRate);

        if (!_computationService.IsSolvable(customer, priceOrder))
            return BadRequest(new ApiResponse<IEnumerable<PortfolioElementDto>>
                (HttpStatusCode.BadRequest, $"Enough money for this action, you don't have"));

        var portfolioElement = new PortfolioElement
        {
            ActionId = action.Id,
            CustomerId = customer.Id,
            BuyPrice = orderCharacteristics.Price,
            PortfolioQuantity = orderCharacteristics.Quantity
        };

        customer.AccountValue -= priceOrder * orderCharacteristics.Quantity;

        var element =
            customer
                .PortfolioElements
                .FirstOrDefault(x => x.CustomerId == customer.Id && x.ActionId == action.Id);

        if (element == null)
            customer.PortfolioElements.Add(portfolioElement);
        else
        {
            var initQuantity = element.PortfolioQuantity;
            element.PortfolioQuantity += orderCharacteristics.Quantity;
            element.BuyPrice = (element.BuyPrice * initQuantity + orderCharacteristics.Price * orderCharacteristics.Quantity) /
                               element.PortfolioQuantity;
            _unitOfWork.Repository<Customer>().Update(customer);
        }
        
        await _unitOfWork.CompleteAsync();

        return Ok(new ApiResponse<PortfolioElementDto>
        {
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Content = _mapper.Map(customer.PortfolioElements.FirstOrDefault(x => x.ActionId == action.Id))
        });
    }

    [HttpPost("sell")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<PortfolioElementDto>>> SellAction(
        [FromBody] OrderStockActionDto sellStockActionDto)
    {
        var customer = await _unitOfWork.Repository<Customer>()
            .GetEntityWithSpec(new CustomerWithPortfolioElementSpecification(sellStockActionDto.ClientId));

        if (customer == null)
            return BadRequest(new ApiResponse<IEnumerable<PortfolioElementDto>>
                (HttpStatusCode.BadRequest, $"Found, the Customer {sellStockActionDto.ClientId} was not"));

        var action = await _unitOfWork.Repository<StockAction>()
            .GetEntityWithSpec(new StockActionOfSymbolSpecification(sellStockActionDto.Symbol));

        if (action == null)
            return BadRequest(new ApiResponse<IEnumerable<PortfolioElementDto>>
                (HttpStatusCode.BadRequest, $"Found, the action {sellStockActionDto.Symbol} was not"));

        var orderBook = await _unitOfWork.Repository<OrderBook>()
            .GetEntityWithSpec(new OrderBookByIdWithOrdersSpecification(action.Id));

        if (orderBook == null)
            return BadRequest(new ApiResponse<IEnumerable<PortfolioElementDto>>
                (HttpStatusCode.BadRequest, $"Found, the OrderBook of {sellStockActionDto.Symbol} was not"));
        
        var element =
            customer.PortfolioElements.FirstOrDefault(x => x.CustomerId == customer.Id && x.ActionId == action.Id);

        if (element == null)
            return BadRequest(new ApiResponse<IEnumerable<PortfolioElementDto>>
                (HttpStatusCode.BadRequest, $"At your disposal, the action {action.Symbol} was not"));

        var forexRate = await _unitOfWork.Repository<ForexRate>()
            .GetEntityWithSpec(new ForexRateWithCurrencySpecification(action.TradingCurrency));

        if (forexRate == null)
            return BadRequest(new ApiResponse<IEnumerable<PortfolioElementDto>>
                (HttpStatusCode.BadRequest, $"Found, the Forex of {action.TradingCurrency} was not"));

        if (!_computationService.IsValid(element, sellStockActionDto.Quantity))
            return BadRequest(new ApiResponse<IEnumerable<PortfolioElementDto>>
                (HttpStatusCode.BadRequest, "Enough actions in your portfolio, you don't have"));

        var orderCharacteristics = _computationService.OrderBookPrice(orderBook, customer.Id, sellStockActionDto.Quantity, OrderType.Buy);
        
        foreach (var orderBookElement in orderBook.OrderElements)
        {
            if(orderBookElement.Quantity == 0)
                _unitOfWork.Repository<Order>().Delete(orderBookElement);
            else
                _unitOfWork.Repository<Order>().Update(orderBookElement);
        }

        action.MarketPrice = orderCharacteristics.Price;
        _unitOfWork.Repository<StockAction>().Update(action);

        
        var priceOrder = _computationService.PriceToEur(orderCharacteristics.Price, forexRate);
        element.PortfolioQuantity -= orderCharacteristics.Quantity;
        customer.AccountValue += priceOrder * orderCharacteristics.Quantity;
        
        if (element.PortfolioQuantity == 0)
            _unitOfWork.Repository<PortfolioElement>().Delete(element);

        _unitOfWork.Repository<Customer>().Update(customer);

        var res = customer.PortfolioElements.FirstOrDefault(x => x.ActionId == action.Id);

        await _unitOfWork.CompleteAsync();

        return Ok(new ApiResponse<PortfolioElementDto>
        {
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Content = _mapper.Map(res)
        });
    }
}