using DomainHistoryEF.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DomainHistoryEF.Infra.Data.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
    }
}
