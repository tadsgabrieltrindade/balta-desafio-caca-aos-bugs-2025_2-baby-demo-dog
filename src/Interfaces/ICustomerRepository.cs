using BugStore.Models;

namespace BugStore.Interfaces
{
    public interface ICustomerRepository
    {
        Task Create(Customer customer);
        Task Delete(Customer customer);
        Task<List<Customer?>> Get();
        Task<Customer?> GetById(Guid id);
        Task<bool> Update(Customer customer);
    }
}
