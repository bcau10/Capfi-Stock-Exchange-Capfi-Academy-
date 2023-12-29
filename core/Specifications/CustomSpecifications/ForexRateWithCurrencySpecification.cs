using core.Model;
using core.Utils;

namespace core.Specifications.CustomSpecifications;
public class ForexRateWithCurrencySpecification : BaseSpecification<ForexRate>
{
    public ForexRateWithCurrencySpecification(TradingCurrency tradingCurrency) 
        : base(x => x.TradingCurrency == tradingCurrency)
    { }
}
