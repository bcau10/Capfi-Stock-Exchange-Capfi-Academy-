using core.Model;
using core.Utils;

namespace core.Specifications.SpecificationParams;

public class StockActionSpecificationParams
{
    private const int MaxPageSize = 40;
    public int PageIndex { get; set; } = 1;
    public string Sort { get; set; }
    public TradingCurrency? TradingCurrency { get; set; }
    public string Symbol { get; set; }
    public string Isin { get; set; }
    
    public int PageSize
    {
        get => _pageSie;
        set => _pageSie = value > MaxPageSize ? MaxPageSize : value;
    }

    private int _pageSie = 10;
}