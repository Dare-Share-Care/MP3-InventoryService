using Ardalis.Specification;
using Inventory.Web.Entity;

namespace Inventory.Web.Specifications;

public sealed class ProductByIdSpec : Specification<Product>
{
    public ProductByIdSpec(int id)
    {
        Query.Where(p => p.Id == id);
    }
    
}