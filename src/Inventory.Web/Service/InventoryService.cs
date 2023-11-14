using System.Diagnostics;
using Ardalis.Specification;
using Inventory.Web.Entity;
using Inventory.Web.Interfaces.DomainServices;
using Inventory.Web.Interfaces.Repositories;
using Inventory.Web.Model.Dto;
using Inventory.Web.Producer;
using Inventory.Web.Specifications;

namespace Inventory.Web.Service;

public class InventoryService : IInventoryService
{
    private readonly IReadRepository<Product> _productReadRepository;
    private readonly RequestSupplies _requestSupplies;

    public InventoryService(IReadRepository<Product> productReadRepository, RequestSupplies requestSupplies)
    {
        _productReadRepository = productReadRepository;
        _requestSupplies = requestSupplies;
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
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Quantity = product.Quantity,
            Price = product.Price
        };
    }
    
    public async Task RequestSuppliesAsync(ProductDto productDto)
    {
        if (productDto.Quantity < 100)
        {
            var requestSuppliesDto = new RequestSuppliesDto
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Quantity = 5000 - productDto.Quantity
            };
            await _requestSupplies.ProduceAsync("mp3-request-restock", requestSuppliesDto);
        }
    }
}
