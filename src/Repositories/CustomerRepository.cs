using BugStore.Data;
using BugStore.Interfaces;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Customer customer)
        {
           await _context.Customers.AddAsync(customer);
        }

        public async Task Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task<List<Customer?>> Get()
        {
            return await _context.Customers.ToListAsync();
        }

        public Task<Customer?> GetById(Guid id)
        {
            if(id == Guid.Empty)
                return Task.FromResult<Customer?>(null);
            return _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<bool> Update(Customer customer)
        {
            if (customer == null || customer.Id == Guid.Empty)
                return Task.FromResult(false);
            _context.Customers.Update(customer);
            return Task.FromResult(true);
        }
    }
}
