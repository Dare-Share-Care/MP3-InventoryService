using Ardalis.Specification.EntityFrameworkCore;
using Inventory.Web.Interfaces.Repositories;

namespace Inventory.Web.Data;

public class EfRepository<T> : RepositoryBase<T>, IRepository<T>, IReadRepository<T> where T : class
{
    public readonly InventoryContext _dbContext;
    
    public EfRepository(InventoryContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
}
