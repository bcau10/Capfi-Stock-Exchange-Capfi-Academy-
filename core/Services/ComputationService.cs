using core.Model;
using core.Utils;

namespace core.Services;

public class ComputationService : IComputationService
{
    public double PriceToEur(double marketPrice, ForexRate forexRate)
        => marketPrice / forexRate.Rate;
    public bool IsSolvable(Customer customer, double priceOrder)
        => customer.AccountValue > priceOrder;
    public bool IsValid(PortfolioElement portfolioElement, double quantity)
        => portfolioElement.PortfolioQuantity >= quantity;

    public OrderCharacteristics OrderBookPrice(OrderBook orderBook, int customerId, int quantity, OrderType orderType)
    {
        var orders = orderBook.OrderElements.Where(x => x.OrderType == orderType && x.CustomerId != customerId);
        var sortedOrders = orderType == OrderType.Sell 
            ? orders.OrderBy(x => x.Price).AsEnumerable()
            : orders.OrderByDescending(x => x.Price).AsEnumerable();
        
        var boughtQuantity = 0;
        var weightedPrice = 0.0;
        var maxOrdersCount = sortedOrders.Sum(x => x.Quantity);
        
        while(boughtQuantity != maxOrdersCount && quantity != 0 )
        {
            var currentAction = sortedOrders.First();
            var currentBuyQuantity = Math.Min(currentAction.Quantity, quantity);
            boughtQuantity += currentBuyQuantity;
            currentAction.Quantity -=  currentBuyQuantity;
            weightedPrice += currentAction.Price * currentBuyQuantity;
            quantity -= currentBuyQuantity;
            sortedOrders = sortedOrders.Skip(1);
        }

        var orderCharacteristics = new OrderCharacteristics(boughtQuantity, weightedPrice / boughtQuantity);
        return orderCharacteristics;
    }

}