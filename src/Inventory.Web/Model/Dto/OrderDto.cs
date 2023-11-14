namespace Inventory.Web.Model.Dto;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public List<OrderLineDto> Lines { get; set; } = new();
}