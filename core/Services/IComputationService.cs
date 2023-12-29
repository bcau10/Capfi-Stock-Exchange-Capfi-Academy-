using core.Model;
using core.Repositories;
using core.Utils;

namespace core.Services;

public interface IComputationService
{
    public double PriceToEur(double marketPrice, ForexRate forexRate);
    public bool IsSolvable(Customer customer, double priceOrder);
    public bool IsValid(PortfolioElement portfolioElement, double quantity);
    public OrderCharacteristics OrderBookPrice(OrderBook orderBook, int customerId, int quantity, OrderType orderType);
}
