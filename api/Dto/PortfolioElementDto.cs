namespace api.Dto;

public class PortfolioElementDto
{
    public int Id { get; set; }
    public int ActionId { get; set; }
    public int PortfolioQuantity { get; set; }
    public int CustomerId { get; set; }
    public double BuyPrice { get; set; }
}