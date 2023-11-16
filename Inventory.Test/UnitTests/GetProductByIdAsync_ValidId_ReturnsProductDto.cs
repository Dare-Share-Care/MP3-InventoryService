using Inventory.Web.Entity;
using Inventory.Web.Interfaces.Repositories;
using Inventory.Web.Service;
using Inventory.Web.Specifications;
using Moq;

namespace Inventory.Test.UnitTests;

public class InventoryServiceTests
{
    [Fact]
    public async Task GetProductByIdAsync_ValidId_ReturnsProductDto()
    {
        // Arrange
        var mockRepo = new Mock<IReadRepository<Product>>();
        mockRepo.Setup(repo => repo.FirstOrDefaultAsync(
                It.IsAny<ProductByIdSpec>(),
                It.IsAny<CancellationToken>())) // Assuming CancellationToken is the optional parameter
            .ReturnsAsync(new Product { Id = 1, Name = "Test Product", Quantity = 10, Price = 100 });
        var service = new InventoryService(mockRepo.Object, null, null);

        // Act
        var result = await service.GetProductByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Product", result.Name);
    }
}