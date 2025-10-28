using BugStore.Models;

namespace BugStore.Interfaces
{
    public interface IOrderRepository
    {

        Task<bool> Create(Order order);
        Task<Order?> GetById(Guid id);
    }
}
