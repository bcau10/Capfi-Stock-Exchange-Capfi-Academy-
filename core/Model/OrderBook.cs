using System.ComponentModel.DataAnnotations;

namespace core.Model;

public class OrderBook : BaseEntity
{
    [Required]
    public int StockActionId { get; set; }
    public StockAction StockAction { get; set; }
    public ICollection<Order> OrderElements { get; set; } = new List<Order>();
}