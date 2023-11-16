using Inventory.Web.Controller;
using Inventory.Web.Interfaces.DomainServices;
using Inventory.Web.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Inventory.Test.IntegrationTests;

public class InventoryControllerTests
{
    [Fact]
    public async Task GetProductsAsync_ReturnsListOfProductDtos()
    {
        // Arrange
        var mockService = new Mock<IInventoryService>();
        mockService.Setup(service => service.GetProductsAsync())
            .ReturnsAsync(new List<ProductDto> { new ProductDto { Id = 1, Name = "Test Product" } });

        var controller = new InventoryController(mockService.Object);

        // Act
        var result = await controller.GetProductsAsync();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ProductDto>>(actionResult.Value);
        Assert.Single(returnValue);
    }
}
