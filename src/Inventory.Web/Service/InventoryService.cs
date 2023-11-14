using Inventory.Web.Entity;
using Inventory.Web.Interfaces.Repositories;
using Inventory.Web.Interfaces.DomainServices;
using Inventory.Web.Model.Dto;
using Inventory.Web.Producer;
using Inventory.Web.Specifications;

namespace Inventory.Web.Service;

public class InventoryService : IInventoryService
{
    private readonly IReadRepository<Product> _productReadRepository;
    private readonly IRepository<Product> _productRepository; 
    private readonly RequestSupplies _requestSupplies;

    public InventoryService(IReadRepository<Product> productReadRepository, RequestSupplies requestSupplies, IRepository<Product> productRepository)
    {
        _productReadRepository = productReadRepository;
        _requestSupplies = requestSupplies;
        _productRepository = productRepository;
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
    
    // i want to make UpdateProductQuantityAsync function to be called from the consumer
    public async Task UpdateProductQuantityAsync(int id, int quantity)
    {
        var product = await _productReadRepository.FirstOrDefaultAsync(new ProductByIdSpec(id));
        product.Quantity += quantity;
        await _productRepository.SaveChangesAsync();
    }
}