using core.Utils;
using System.ComponentModel.DataAnnotations;

namespace core.Model;

public class ForexRate : BaseEntity
{
    [Required]
    public TradingCurrency TradingCurrency { get; set; }
    [Required]
    [Range(0.0d, double.MaxValue)]
    public double Rate { get; set; }
}
