using Ardalis.Specification;
using Inventory.Web.Entity;

namespace Inventory.Web.Specifications;

public sealed class ProductByNameSpec : Specification<Product>
{
    public ProductByNameSpec(string name)
    {
        Query.Where(p => p.Name == name);
    }
}