using BugStore.Data;
using BugStore.Interfaces;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<List<Product?>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        public Task<Product?> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Task.FromResult<Product?>(null);
            return _context.Products.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<bool> Update(Product product)
        {
            if (product == null || product.Id == Guid.Empty)
                return Task.FromResult(false);
            _context.Products.Update(product);
            return Task.FromResult(true);
        }
    }
}
