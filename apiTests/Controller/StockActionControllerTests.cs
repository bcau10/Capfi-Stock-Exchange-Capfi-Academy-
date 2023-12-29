using api.Controllers.v1;
using api.Dto;
using api.Utilities;
using core.Model;
using core.Repositories;
using NSubstitute;
using System.Net;
using core.Services;

namespace apiTests.Controller;

// [TestFixture]
public class StockActionsControllerTests
{
    // private StockActionsController _stockActionsController;
    // private IMapperBase _mapperMock;
    // private IUnitOfWork _unitOfWorkMock;
    // private IComputationService _computationService;
    //
    //
    // [SetUp]
    // public void Setup()
    // {
    //     _mapperMock = Substitute.For<IMapperBase>();
    //     _unitOfWorkMock = Substitute.For<IUnitOfWork>();
    //     _computationService = Substitute.For<IComputationService>();
    //     _stockActionsController = new StockActionsController(_unitOfWorkMock, _mapperMock,_computationService);
    //
    // }
    //
    // [Test]
    // public async Task GetActionsAsync_ReturnsApiResponseWithStatusCodeOk()
    // {
    //     // Given
    //     var stockActions = new List<StockAction>();
    //     var stockActionDtos = new List<StockActionDto>();
    //
    //     _unitOfWorkMock.Repository<StockAction>().GetListAllAsync().Returns(stockActions);
    //     _mapperMock.MapList(stockActions).Returns(stockActionDtos);
    //
    //     // When
    //     var response = await _stockActionsController.GetActionsAsync();
    //
    //     // Then
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(response.Value.IsSuccess, Is.True);
    //         Assert.That(response.Value.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    //         Assert.That(response.Value.Content, Is.EqualTo(stockActionDtos));
    //     });
    // }
    //
    // [Test]
    // public async Task GetActionByIdAsync_WithValidId_ReturnsApiResponseWithStatusCodeOk()
    // {
    //     // Given
    //     int validId = 1;
    //     var stockAction = new StockAction { Id = validId };
    //     var stockActionDto = new StockActionDto();
    //
    //     _unitOfWorkMock.Repository<StockAction>().GetByIdAsync(validId).Returns(stockAction);
    //     _mapperMock.Map(stockAction).Returns(stockActionDto);
    //
    //     // When
    //     var response = await _stockActionsController.GetActionByIdAsync(validId);
    //
    //     // Then
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(response.Value.IsSuccess, Is.True);
    //         Assert.That(response.Value.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    //         Assert.That(response.Value.Content, Is.EqualTo(stockActionDto));
    //     });
    // }
    //
    // [Test]
    // public async Task GetActionByIdAsync_WithInvalidId_ReturnsApiResponseWithStatusCodeNotFound()
    // {
    //     // Given
    //     int invalidId = -1;
    //
    //     _unitOfWorkMock.Repository<StockAction>()
    //         .GetByIdAsync(invalidId)
    //         .Returns((StockAction)null);
    //
    //     // When
    //     var response = await _stockActionsController.GetActionByIdAsync(invalidId);
    //
    //     // Then
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(response.Value.IsSuccess, Is.False);
    //         Assert.That(response.Value.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    //         Assert.That(response.Value.Content, Is.Null);
    //     });
    // }

    //[Test]
    //public async Task BuyAction_WithSufficientFunds_ReturnsApiResponseWithStatusCodeOk()
    //{
    //    var buyActionDto = new OrderStockActionDto
    //    {
    //        ClientId = 1,
    //        Symbol = "AAPL", 
    //        Quantity = 10
    //    };

    //    var customer = new Customer
    //    {
    //        Id = buyActionDto.ClientId,
    //        AccountValue = 100000
    //    };
    //    customer.PortfolioElements = new List<PortfolioElement>{new PortfolioElement
    //    {
    //        ActionId = 1, 
    //        PortfolioQuantity = 100, 
    //        BuyPrice = 150.0, 
    //        CustomerId = 2
    //    } };

    //    var action = new StockAction
    //    {
    //        Id = 1, 
    //        MarketPrice = 150,
    //        TradingCurrency = core.Utils.TradingCurrency.USD 
    //    };

    //    unitOfWorkMock.Repository<Customer>()
    //        .GetEntityWithSpec(Arg.Any<CustomerWithPortfolioElementSpecification>())
    //        .Returns(customer);

    //    unitOfWorkMock.Repository<StockAction>()
    //        .GetEntityWithSpec(Arg.Any<StockActionWithPagingAndFilteringSpecification>())
    //    .Returns(action);

    //    computationService.IsSolvable(Arg.Any<Customer>(), Arg.Any<double>())
    //        .Returns(true);

    //    // When
    //    var response = await stockActionsController.BuyAction(buyActionDto);

    //    // Then
    //    Assert.Multiple(() =>
    //    {
    //        Assert.That(response.IsSuccess, Is.True);
    //        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    //    });
    //}

    //[Test]
    //public async Task BuyAction_WithInsufficientFunds_ReturnsApiResponseWithStatusCodeBadRequest()
    //{
    //    // Given
    //    var buyActionDto = new OrderStockActionDto
    //    {
    //        ClientId = 1,
    //        Symbol = "AAPL",
    //        Quantity = 10
    //    };

    //    var customer = new Customer
    //    {
    //        Id = buyActionDto.ClientId,
    //        AccountValue = 50
    //    };

    //    var action = new StockAction
    //    {
    //        Id = 1, 
    //        MarketPrice = 150, 
    //        TradingCurrency = core.Utils.TradingCurrency.USD 
    //    };

    //    unitOfWorkMock.Repository<Customer>()
    //        .GetEntityWithSpec(Arg.Any<CustomerWithPortfolioElementSpecification>())
    //        .Returns(customer);

    //    unitOfWorkMock.Repository<StockAction>()
    //        .GetEntityWithSpec(Arg.Any<StockActionWithPagingAndFilteringSpecification>())
    //    .Returns(action);

    //    computationService.IsSolvable(Arg.Any<Customer>(), Arg.Any<double>())
    //        .Returns(false);

    //    // When
    //    var response = await stockActionsController.BuyAction(buyActionDto);

    //    // Then
    //    Assert.Multiple(() =>
    //    {
    //        Assert.That(response.IsSuccess, Is.False);
    //        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
           
    //    });
    //}
    
    //[Test]
    //public async Task SellAction_WithValidData_ReturnsApiResponseWithStatusCodeOk()
    //{
    //    // Given
    //    var sellStockActionDto = new OrderStockActionDto
    //    {
    //        ClientId = 1,
    //        Symbol = "AAPL", 
    //        Quantity = 10
    //    };

    //    var customer = new Customer
    //    {
    //        Id = sellStockActionDto.ClientId,
    //        AccountValue = 1000
    //    };
    //    customer.PortfolioElements = new List<PortfolioElement>{new PortfolioElement
    //    {
    //        ActionId = 1,
    //        PortfolioQuantity = 100, 
    //        BuyPrice = 150.0, 
    //        CustomerId = 2 
    //    } };
    //    var action = new StockAction
    //    {
    //        Id = 1, 
    //        MarketPrice = 150, 
    //        TradingCurrency = core.Utils.TradingCurrency.USD 
    //    };

    //    var portfolioElement = new PortfolioElement
    //    {
    //        ActionId = action.Id,
    //        PortfolioQuantity = 20 
    //    };

    //    customer.PortfolioElements.Add(portfolioElement);

    //    unitOfWorkMock.Repository<Customer>()
    //        .GetEntityWithSpec(Arg.Any<CustomerWithPortfolioElementSpecification>())
    //        .Returns(customer);

    //    unitOfWorkMock.Repository<StockAction>()
    //        .GetEntityWithSpec(Arg.Any<StockActionOfSymbolSpecification>())
    //        .Returns(action);

    //    computationService.IsValid(Arg.Any<PortfolioElement>(), Arg.Any<int>())
    //.Returns(true);


    //    // When
    //    var response = await stockActionsController.SellAction(sellStockActionDto);

    //    // Then
    //    Assert.Multiple(() =>
    //    {
    //        Assert.That(response.IsSuccess, Is.True);
    //        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    //    });
    //}

    //[Test]
    //public async Task SellAction_WithInsufficientPortfolioQuantity_ReturnsApiResponseWithStatusCodeBadRequest()
    //{
    //    // Given
    //    var sellStockActionDto = new OrderStockActionDto
    //    {
    //        ClientId = 1,
    //        Symbol = "AAPL", 
    //        Quantity = 10
    //    };

    //    var customer = new Customer
    //    {
    //        Id = sellStockActionDto.ClientId,
    //        AccountValue = 1000
    //    };
    //    customer.PortfolioElements = new List<PortfolioElement>{new PortfolioElement
    //    {
    //        ActionId = 1,
    //        PortfolioQuantity = 100,
    //        BuyPrice = 150.0,
    //        CustomerId = 2
    //    } };
    //    var action = new StockAction
    //    {
    //        Id = 1, 
    //        MarketPrice = 150, 
    //        TradingCurrency = core.Utils.TradingCurrency.USD
    //    };

    //    var portfolioElement = new PortfolioElement
    //    {
    //        ActionId = action.Id,
    //        PortfolioQuantity = 5 
    //    };

    //    customer.PortfolioElements.Add(portfolioElement);

    //    unitOfWorkMock.Repository<Customer>()
    //        .GetEntityWithSpec(Arg.Any<CustomerWithPortfolioElementSpecification>())
    //        .Returns(customer);

    //    unitOfWorkMock.Repository<StockAction>()
    //        .GetEntityWithSpec(Arg.Any<StockActionOfSymbolSpecification>())
    //        .Returns(action);

    //    computationService.IsValid(Arg.Any<PortfolioElement>(), Arg.Any<int>())
    // .Returns(false);


    //    // When
    //    var response = await stockActionsController.SellAction(sellStockActionDto);

    //    // Then
    //    Assert.Multiple(() =>
    //    {
    //        Assert.That(response.IsSuccess, Is.False);
    //        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
 
    //    });
    //}
}
