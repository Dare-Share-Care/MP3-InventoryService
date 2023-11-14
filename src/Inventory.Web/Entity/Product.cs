namespace Inventory.Web.Entity;

public class Product : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
}