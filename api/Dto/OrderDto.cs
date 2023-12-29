using core.Utils;

namespace api.Dto;

public class OrderDto
{
    public int Id { get; set; }
    public int OrderBookId { get; set; }
    public OrderType OrderType { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public int CustomerId { get; set; }
}