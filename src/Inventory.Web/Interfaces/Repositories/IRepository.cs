using Ardalis.Specification;

namespace Inventory.Web.Interfaces.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
    
}