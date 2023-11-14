namespace Inventory.Web.Model.Dto;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }

    public decimal Price { get; set; }
}