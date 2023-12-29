using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using core.Utils;

namespace core.Model;

public class StockAction : BaseEntity
{
    [MinLength(1)]
    [Required]
    public string Isin { get; init; }

    [MinLength(1)]
    [MaxLength(12)]
    [Required]
    public string Title { get; init; }

    [MinLength(1)]
    [MaxLength(10)]
    [Required]
    public string Symbol { get; init; }

    [MinLength(1)]
    [MaxLength(255)]
    [Required]
    public string ListingMarket { get; init; }

    [Required]
    public TradingCurrency TradingCurrency { get; init; }

    public string Index { get; set; }
    
    [NotMapped]
    public dynamic Misc { get; set; }

    [Range(1, int.MaxValue)]
    [Required]
    public int Quantity { get; init; }

    [Range(0.0d, double.MaxValue)]
    [Required]
    public double MarketPrice { get; set; }

    public OrderBook OrderBook { get; set; }
}
