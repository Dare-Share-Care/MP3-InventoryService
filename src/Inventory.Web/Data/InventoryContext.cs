using Inventory.Web.Entity;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Web.Data;

public class InventoryContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    
    public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // set primary keys
        modelBuilder.Entity<Product>().HasKey(p => p.Id);
    }
}