using JDMarketSLn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Infraestructure.Persistence.Contexts.Configurations
{
    public class UnitMeasureProductConfigurations : IEntityTypeConfiguration<UnitMeasureProduct>
    {
        public void Configure(EntityTypeBuilder<UnitMeasureProduct> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Unit).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Description).HasColumnType("varchar(max)");

            builder.HasMany<ProductDetail>(s => s.ProductDetail).WithOne(g => g.UnitMeasureProduct).HasForeignKey(s => s.UnitMeasureProductId).OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
        }
    }
}
