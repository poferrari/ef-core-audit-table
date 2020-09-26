using DomainHistoryEF.Domain.Catalogs.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainHistoryEF.Domain.Catalogs.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();

        Task<Product> GetById(Guid id);

        Task<bool> Add(Product product);

        Task<bool> Update(Product product);
    }
}
