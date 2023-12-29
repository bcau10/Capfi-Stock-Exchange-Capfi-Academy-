using core.Model;

namespace core.Specifications.CustomSpecifications;

public class StockActionWithSymbolSpecification : BaseSpecification<StockAction>
{
    public StockActionWithSymbolSpecification(string symbol)
        : base (x => x.Symbol == symbol)
    { }
}
