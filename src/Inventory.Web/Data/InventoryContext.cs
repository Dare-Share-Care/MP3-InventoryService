using Inventory.Web.Entity;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Web.Data;

public class InventoryContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    
    public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        // Set Primary Keys
        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        
        // Set Properties
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
        
        // Set Seed Data
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Apple", Quantity = 10, Price = 1.99m },
            new Product { Id = 2, Name = "Banana", Quantity = 20, Price = 2.99m },
            new Product { Id = 3, Name = "Orange", Quantity = 30, Price = 3.99m }
        );
    }
}