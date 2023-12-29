using System.ComponentModel.DataAnnotations;

namespace api.Dto;

public class OrderStockActionDto
{
    [Required]
    public int ClientId { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    public string Symbol { get; set; }
}