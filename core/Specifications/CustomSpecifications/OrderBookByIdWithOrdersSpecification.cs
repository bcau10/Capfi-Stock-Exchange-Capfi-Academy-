using core.Model;

namespace core.Specifications.CustomSpecifications;

public class OrderBookByIdWithOrdersSpecification : BaseSpecification<OrderBook>
{
    public OrderBookByIdWithOrdersSpecification(int actionId)
        : base (x => x.Id == actionId)
        =>   AddInclude(x => x.OrderElements);
}