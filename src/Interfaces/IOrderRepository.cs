using BugStore.Models;

namespace BugStore.Interfaces
{
    public interface IOrderRepository
    {

        Task Create(Order order);
        Task Delete(Order order);
        Task<List<Order?>> Get();
        Task<Order?> GetById(Guid id);
        Task<bool> Update(Order order);
    }
}
