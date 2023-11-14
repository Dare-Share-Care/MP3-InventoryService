using System.Diagnostics;
using Inventory.Web.Entity;
using Inventory.Web.Interfaces.DomainServices;
using Inventory.Web.Interfaces.Repositories;
using Inventory.Web.Model.Dto;
using Inventory.Web.Specifications;

namespace Inventory.Web.Service;

public class InventoryService : IInventoryService
{
    private readonly IReadRepository<Product> _productReadRepository;

    public InventoryService(IReadRepository<Product> productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<List<ProductDto>> GetProductsAsync()
    {
        var products = await _productReadRepository.ListAsync();
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Quantity = p.Quantity,
            Price = p.Price
        }).ToList();
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productReadRepository.FirstOrDefaultAsync(new ProductByIdSpec(id));
        Debug.Assert(product != null, nameof(product) + " != null");
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Quantity = product.Quantity,
            Price = product.Price
        };
    }
}