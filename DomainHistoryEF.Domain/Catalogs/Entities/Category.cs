using System;
using System.Collections.Generic;

namespace DomainHistoryEF.Domain.Catalogs.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
