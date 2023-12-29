using api.Dto;
using core.Model;

namespace api.Utilities;

public interface IMapperBase
{
    StockActionDto Map(StockAction stockAction);
    PortfolioElementDto Map(PortfolioElement portfolioElement);
    IEnumerable<CustomerDto> MapList(IEnumerable<Customer> customers);
    IEnumerable<StockActionDto> MapList(IEnumerable<StockAction> stockActions);
    IEnumerable<PortfolioElementDto> MapList(IEnumerable<PortfolioElement> portfolioElements);
    IEnumerable<OrderBookDto> MapList(IEnumerable<OrderBook> orderBooks);
    IEnumerable<OrderDto> MapList(IEnumerable<Order> orders);
}
