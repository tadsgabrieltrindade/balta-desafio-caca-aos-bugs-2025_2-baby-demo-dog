using BugStore.Models;

namespace BugStore.Interfaces
{
    public interface IProductRepository
    {
        Task Create(Product product);
        Task Delete(Product product);
        Task<List<Product?>> Get();
        Task<Product?> GetById(Guid id);
        Task<bool> Update(Product product);
    }
}
