using core.Model;
using core.Specifications.SpecificationParams;

namespace core.Specifications.CustomSpecifications;

public class StockActionWithFilteringSpecification : BaseSpecification<StockAction>
{
    public StockActionWithFilteringSpecification(StockActionSpecificationParams stockActionParams)
        : base(x =>
            (stockActionParams.TradingCurrency == null 
             || x.TradingCurrency == stockActionParams.TradingCurrency)
            && (string.IsNullOrEmpty(stockActionParams.Symbol) ||
                x.Symbol.ToLower().Contains(stockActionParams.Symbol.ToLower()))
            && (string.IsNullOrEmpty(stockActionParams.Isin) ||
                x.Isin.ToLower().Contains(stockActionParams.Isin.ToLower())))
    {
    }
}