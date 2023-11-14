using Inventory.Web.Interfaces.DomainServices;
using Inventory.Web.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controller;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProductsAsync()
    {
        var products = await _inventoryService.GetProductsAsync();
        return Ok(products);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductByIdAsync(int id)
    {
        var product = await _inventoryService.GetProductByIdAsync(id);
        return Ok(product);
    }
    
    // Marked for deletion DEPRECATED!
    [HttpPost]
    public async Task<IActionResult> RequestSuppliesAsync(ProductDto productDto)
    {
        await _inventoryService.RequestSuppliesAsync(productDto);
        return Ok();
    }
}