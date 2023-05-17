using JDMarketSLn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Infraestructure.Persistence.Contexts.Configurations
{
    public class ProductDetailConfigurations : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.StockMin).IsRequired();
            builder.Property(x => x.StockMax).IsRequired();

            builder.Property(x => x.Modelo).HasMaxLength(150);
            builder.Property(x => x.Marca).HasMaxLength(150);
            builder.Property(x => x.DueDate).HasColumnType("datetime");
            builder.Property(x => x.Deprecated).HasDefaultValue(false);

            builder.HasMany<Product>(s => s.Products).WithOne(g => g.ProductDetail).HasForeignKey(s => s.ProductDetailId).OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
        }
    }
}
