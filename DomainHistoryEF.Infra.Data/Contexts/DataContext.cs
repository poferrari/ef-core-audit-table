using DomainHistoryEF.Domain.Configurations.Entities;
using DomainHistoryEF.Domain.Configurations.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DomainHistoryEF.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
         : base(options) { }

        public DbSet<DomainHistory> DomainHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18, 2)");
            }

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await OnBeforeSaveChanges();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private async Task OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries<IAuditEntity>())
            {
                if (entry.State != EntityState.Modified)
                {
                    continue;
                }

                var tableName = entry.Metadata.GetTableName();
                var keys = entry.Properties.Where(t => t.Metadata.IsPrimaryKey()).Select(t => t.CurrentValue.ToString());
                var columnSourceId = string.Join("|", keys);

                await ReadAuditProperties(tableName, columnSourceId, entry.Properties);

                var invalidStates = new[] { EntityState.Unchanged, EntityState.Detached };
                foreach (var referenceEntry in entry.References)
                {
                    if (invalidStates.Contains(referenceEntry.TargetEntry.State))
                    {
                        continue;
                    }

                    //var stateAddReference = referenceEntry.TargetEntry.State == EntityState.Added;
                    await ReadAuditProperties(tableName, columnSourceId, referenceEntry.TargetEntry.Properties);
                }
            }
        }

        private async Task ReadAuditProperties(string tableName, string columnSourceId, IEnumerable<PropertyEntry> properties)
        {
            var auditFields = AuditHelper.GetExcludedAuditProperties();

            foreach (var property in properties)
            {
                if (property.IsTemporary)
                {
                    continue;
                }

                if (property.Metadata.IsPrimaryKey())
                {
                    continue;
                }

                //if ((stateAddReference || property.IsModified) && !auditFields.Any(t => t.Equals(property.Metadata.Name)))
                if (property.IsModified && !auditFields.Any(t => t.Equals(property.Metadata.Name)))
                {
                    var previousValue = property.OriginalValue != null ? property.OriginalValue.ToString() : string.Empty;

                    await InsertDomainHistory(tableName, columnSourceId, previousValue, property);
                }
            }
        }

        private async Task InsertDomainHistory(string tableName, string columnSourceId, string previousValue, PropertyEntry property)
        {
            var auditEntry = new DomainHistory
            {
                TableName = tableName,
                ColumnName = property.Metadata.GetColumnName(),
                ColumnSourceId = columnSourceId,
                ColumnPreviousValue = previousValue,
                CreatedAtDate = DateTime.Now,
                CreatedBy = $"user.teste_alteracao"
            };

            await DomainHistory.AddAsync(auditEntry);
        }
    }
}
