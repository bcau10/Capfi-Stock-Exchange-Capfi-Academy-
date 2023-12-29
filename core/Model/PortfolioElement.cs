using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core.Model
{
    public class PortfolioElement : BaseEntity
    {
        [ForeignKey(nameof(StockAction))]
        [Required]
        public int ActionId { get; set; }
        public StockAction StockAction { get; set; }
        [Range(0, int.MaxValue)]
        [Required]
        public int PortfolioQuantity { get; set; }
        [Range(0.0d, double.MaxValue)]
        [Required]
        public double BuyPrice { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
