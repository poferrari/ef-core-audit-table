using DomainHistoryEF.Domain.Configurations.Constants;
using DomainHistoryEF.Domain.Configurations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainHistoryEF.Infra.Data.Mappings
{
    public class DomainHistoryMap : IEntityTypeConfiguration<DomainHistory>
    {
        public void Configure(EntityTypeBuilder<DomainHistory> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .IsRequired()
                .HasColumnName("DOMAIN_HISTORY_ID");

            builder.Property(c => c.TableName)
                .IsRequired()
                .HasMaxLength(MapConst.DescriptionLimit)
                .HasColumnName("TABLE_NM");

            builder.Property(c => c.ColumnName)
                .IsRequired()
                .HasMaxLength(MapConst.DescriptionLimit)
                .HasColumnName("COLUMN_NM");

            builder.Property(c => c.ColumnSourceId)
                .IsRequired()
                .HasMaxLength(MapConst.DescriptionLimit)
                .HasColumnName("COLUMN_SOURCE_ID");

            builder.Property(c => c.ColumnPreviousValue)
                .IsRequired()
                .HasMaxLength(MapConst.DescriptionLimit)
                .HasColumnName("COLUMN_PREVIOUS_VL");

            builder.Property(c => c.CreatedAtDate)
                .IsRequired()
                .HasColumnName("CREATED_BY_DT");

            builder.Property(c => c.CreatedBy)
                .IsRequired()
                .HasMaxLength(MapConst.DescriptionLimit)
                .HasColumnName("CREATED_BY_DS");


            builder.ToTable("DOMAIN_HISTORY");
        }
    }
}
