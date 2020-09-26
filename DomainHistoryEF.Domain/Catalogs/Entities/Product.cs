using DomainHistoryEF.Domain.Configurations.Entities;
using System;

namespace DomainHistoryEF.Domain.Catalogs.Entities
{
    public class Product : IAuditEntity
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAtDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatededAtDate { get; set; }
        public string UpdatedByUser { get; set; }

        public virtual Dimension Dimension { get; set; }
        public virtual Category Category { get; set; }
    }
}
