using DomainHistoryEF.Domain.Catalogs.Entities;
using DomainHistoryEF.Domain.Catalogs.Repositories;
using DomainHistoryEF.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainHistoryEF.Infra.Data.Repositories.Catalogs
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetAll()
            => await _dbSet.ToListAsync();

        public async Task<Product> GetById(Guid id)
            => await _dbSet.FirstOrDefaultAsync(t => t.Id == id);

        public async Task<bool> Add(Product product)
        {
            await _dbSet.AddAsync(product);

            return await Save();
        }

        public async Task<bool> Update(Product product)
        {
            product.UpdatedByUser = $"usuarioteste_{DateTime.Now.ToLongTimeString()}";
            product.UpdatededAtDate = DateTime.Now;

            _dbSet.Attach(product);

            return await Save();
        }

        private async Task<bool> Save()
           => await _context.SaveChangesAsync() > 1;
    }
}
