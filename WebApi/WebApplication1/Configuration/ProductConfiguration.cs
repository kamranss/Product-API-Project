using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p=>p.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(p => p.SalePrice).IsRequired(true);
            builder.Property(p => p.CostPrice).IsRequired(true);
            builder.Property(p => p.CostPrice).HasColumnType("decimal(18,2");
            builder.Property(p=>p.CreationDate).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
