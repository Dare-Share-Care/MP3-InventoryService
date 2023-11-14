using Inventory.Web.Entity;
using Inventory.Web.Model.Dto;

namespace Inventory.Web.Interfaces.DomainServices;

public interface IInventoryService
{
    Task<List<ProductDto>> GetProductsAsync();
    
    Task<ProductDto> GetProductByIdAsync(int id);
    
    Task RequestSuppliesAsync(ProductDto productDto);
}