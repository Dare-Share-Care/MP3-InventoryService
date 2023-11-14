using System.Text.Json;
using Confluent.Kafka;
using Inventory.Web.Interfaces.DomainServices;
using Inventory.Web.Model.Dto;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Inventory.Web.Consumer
{
    public class InventoryUpdateConsumer : BackgroundService
    {
        private const string BootstrapServers = "localhost:29094";
        private const string GroupId = "inventory_service_group";
        private const string Topic = "mp3-create-order";

        private readonly IServiceProvider _serviceProvider;

        public InventoryUpdateConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield(); //Find out what this does, cannot compile without
            
            var config = new ConsumerConfig
            {
                GroupId = GroupId,
                BootstrapServers = BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                AllowAutoCreateTopics = true
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(Topic);

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var consumeResult = consumer.Consume(stoppingToken);
                        var order = JsonSerializer.Deserialize<OrderDto>(consumeResult.Message.Value);

                        if (order != null)
                        {
                            using var scope = _serviceProvider.CreateScope();
                            var inventoryService = scope.ServiceProvider.GetRequiredService<IInventoryService>();

                            foreach (var line in order.Lines)
                            {
                                await inventoryService.UpdateProductQuantityAsync(line.ProductId, -line.Quantity);
                            }

                            await CheckAndRequestSuppliesAsync(inventoryService);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Handle cancellation due to stoppingToken
                }
                catch (Exception e)
                {
                    // Handle other exceptions
                }
                finally
                {
                    consumer.Close();
                }
            }
        }

        private async Task CheckAndRequestSuppliesAsync(IInventoryService inventoryService)
        {
            var products = await inventoryService.GetProductsAsync();
            foreach (var product in products)
            {
                if (product.Quantity < 100)
                {
                    await inventoryService.RequestSuppliesAsync(product);
                }
            }
        }
    }
}