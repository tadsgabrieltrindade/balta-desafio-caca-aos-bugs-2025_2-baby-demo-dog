using BugStore.Models;

namespace BugStore.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> Create(Product product);
        Task<bool?> Delete(Product product);
        Task<List<Product?>> Get();
        Task<Product?> GetById(Guid id);
        Task<bool> Update(Product product);
    }
}
