using core.Model;
using core.Specifications.SpecificationParams;

namespace core.Specifications.CustomSpecifications;

public class StockActionWithPagingAndFilteringSpecification : BaseSpecification<StockAction>
{
    public StockActionWithPagingAndFilteringSpecification(StockActionSpecificationParams stockActionParams)
        : base(x =>
            (stockActionParams.TradingCurrency == null 
             || x.TradingCurrency == stockActionParams.TradingCurrency)
            && (string.IsNullOrEmpty(stockActionParams.Symbol) ||
                x.Symbol.ToLower().Contains(stockActionParams.Symbol.ToLower()))
            && (string.IsNullOrEmpty(stockActionParams.Isin) ||
                x.Isin.ToLower().Contains(stockActionParams.Isin.ToLower())))
    {
        ApplyPaging(stockActionParams.PageSize * (stockActionParams.PageIndex - 1), stockActionParams.PageSize);

        switch (stockActionParams.Sort)
        {
            default:
                SetOrderByDescending(x => x.Title);
                break;
        }
    }
}