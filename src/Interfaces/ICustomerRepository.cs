using BugStore.Models;

namespace BugStore.Interfaces
{
    public interface ICustomerRepository
    {
        Task<bool> Create(Customer customer);
        Task<bool?> Delete(Customer customer);
        Task<List<Customer?>> Get();
        Task<Customer?> GetById(Guid id);
        Task<bool> Update(Customer customer);
    }
}
