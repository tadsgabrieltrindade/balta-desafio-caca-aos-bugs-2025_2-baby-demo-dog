using BugStore.Data;
using BugStore.Interfaces;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task Delete(Order order)
        {
            _context.Orders.Remove(order);
        }

        public async Task<List<Order?>> Get()
        {
            return await _context.Orders.ToListAsync();
        }

        public Task<Order?> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Task.FromResult<Order?>(null);
            return _context.Orders.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<bool> Update(Order order)
        {
            if (order == null || order.Id == Guid.Empty)
                return Task.FromResult(false);
            _context.Orders.Update(order);
            return Task.FromResult(true);
        }
    }
}
