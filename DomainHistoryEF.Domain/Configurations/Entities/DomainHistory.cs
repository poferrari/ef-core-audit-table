using System;

namespace DomainHistoryEF.Domain.Configurations.Entities
{
    public class DomainHistory
    {
        public long Id { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ColumnSourceId { get; set; }
        public string ColumnPreviousValue { get; set; }
        public DateTime CreatedAtDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
