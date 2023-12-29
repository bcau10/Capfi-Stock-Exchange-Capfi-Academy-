using core.Utils;
using System.ComponentModel.DataAnnotations;

namespace core.Model;

public class Order : BaseEntity
{
    [Required]
    public OrderType OrderType { get; set; }
    [Range(0.0d, int.MaxValue)]
    [Required]
    public int Quantity { get; set; }
    [Range(0.0d, double.MaxValue)]
    [Required]
    public double Price { get; set; }
    public int OrderBookId { get; set; }
    public OrderBook OrderBook { get; set; }

    public int CustomerId {  get; set; }
    public Customer Customer { get; set; }
}