using DomainHistoryEF.Domain.Catalogs.Entities;
using DomainHistoryEF.Domain.Configurations.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainHistoryEF.Infra.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(MapConst.TextLimit);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(MapConst.DescriptionLimit);

            builder.Property(c => c.Value)
                .IsRequired();

            builder.Property(c => c.Quantity)
                .IsRequired();

            builder.OwnsOne(c => c.Dimension, cm =>
            {
                cm.Property(c => c.Height)
                    .HasColumnName("Height");

                cm.Property(c => c.Width)
                    .HasColumnName("Width");

                cm.Property(c => c.Depth)
                    .HasColumnName("Depth");
            });
        }
    }
}
