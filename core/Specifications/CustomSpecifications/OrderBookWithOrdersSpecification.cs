using core.Model;

namespace core.Specifications.CustomSpecifications;

public class OrderBookWithOrdersSpecification : BaseSpecification<OrderBook>
{
    public OrderBookWithOrdersSpecification()
    {
        AddInclude(x => x.OrderElements);
    }
}