using core.Model;

namespace core.Specifications.CustomSpecifications;

public class StockActionOfSymbolSpecification : BaseSpecification<StockAction>
{
    public StockActionOfSymbolSpecification(string symbol)
        : base(x => x.Symbol.ToLower().Contains(symbol.ToLower()))
    {
    }
}