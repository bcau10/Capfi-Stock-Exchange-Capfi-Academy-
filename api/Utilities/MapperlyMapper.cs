using api.Dto;
using core.Model;
using Riok.Mapperly.Abstractions;

namespace api.Utilities;

[Mapper]
public partial class MapperlyMapper : IMapperBase
{
    public partial StockActionDto Map(StockAction stockAction);
    public partial PortfolioElementDto Map(PortfolioElement portfolioElement);
    public partial IEnumerable<StockActionDto> MapList(IEnumerable<StockAction> stockActions);
    public partial IEnumerable<CustomerDto> MapList(IEnumerable<Customer> customers);
    public partial IEnumerable<PortfolioElementDto> MapList(IEnumerable<PortfolioElement> portfolioElements);
    public partial IEnumerable<OrderBookDto> MapList(IEnumerable<OrderBook> orderBooks);
    public partial IEnumerable<OrderDto> MapList(IEnumerable<Order> orders);
}