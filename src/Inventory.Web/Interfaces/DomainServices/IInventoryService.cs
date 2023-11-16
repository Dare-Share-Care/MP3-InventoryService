using Inventory.Web.Entity;
using Inventory.Web.Model.Dto;

namespace Inventory.Web.Interfaces.DomainServices;

/// <summary>
/// Defines a contract for inventory services, including operations for retrieving and updating product information.
/// </summary>
public interface IInventoryService
{
    /// <summary>
    /// Asynchronously retrieves a list of all products.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="ProductDto"/>.</returns>
    Task<List<ProductDto>> GetProductsAsync();
    
    /// <summary>
    /// Asynchronously retrieves a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="ProductDto"/> if found, otherwise null.</returns>
    Task<ProductDto> GetProductByIdAsync(int id);
    
    /// <summary>
    /// Asynchronously requests supplies if the quantity of a product is below a certain threshold.
    /// </summary>
    /// <param name="productDto">The product data transfer object containing information about the product to check.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RequestSuppliesAsync(ProductDto productDto);
    
    /// <summary>
    /// Asynchronously updates the quantity of a specific product.
    /// </summary>
    /// <param name="productId">The ID of the product to update.</param>
    /// <param name="quantityChange">The amount to adjust the product's quantity by.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateProductQuantityAsync(int productId, int quantityChange);
}