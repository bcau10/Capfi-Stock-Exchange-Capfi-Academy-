namespace core.Utils;

public class OrderCharacteristics
{
    public int Quantity { get; set; }
    public double Price { get; set; }

    public OrderCharacteristics(int quantity, double price)
    {
        Quantity = quantity;
        Price = price;
    }
}