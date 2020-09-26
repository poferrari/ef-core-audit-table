using System;

namespace DomainHistoryEF.Domain.Configurations.Entities
{
    public interface IAuditEntity
    {
        Guid Id { get; set; }
        DateTime CreatedAtDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdatededAtDate { get; set; }
        string UpdatedByUser { get; set; }
    }
}
