using core.Utils;

namespace api.Dto;

public record StockActionDto
{
    public string Isin { get; init; }
    public string Title { get; init; }
    public string Symbol { get; init; }
    public string ListingMarket { get; init; }
    public TradingCurrency TradingCurrency { get; init; }
    public string Index { get; set; }
    public dynamic Misc { get; set; }
    public int Quantity { get; init; }
    public double MarketPrice { get; set; }
}