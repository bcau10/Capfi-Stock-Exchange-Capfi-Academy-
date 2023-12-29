namespace api.Dto;

public class OrderBookDto
{
    public int Id { get; set; }
    public int StockActionId { get; set; }
    public ICollection<OrderDto> OrderElements { get; set; }
}