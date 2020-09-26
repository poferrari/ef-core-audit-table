using DomainHistoryEF.Domain.Catalogs.Entities;
using DomainHistoryEF.Domain.Catalogs.Repositories;
using DomainHistoryEF.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainHistoryEF.Infra.Data.Repositories.Catalogs
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetAll()
           => await _dbSet.ToListAsync();

        public async Task<bool> AddRange(List<Category> categories)
        {
            await _dbSet.AddRangeAsync(categories);

            return await Save();
        }

        private async Task<bool> Save()
           => await _context.SaveChangesAsync() > 1;
    }
}
