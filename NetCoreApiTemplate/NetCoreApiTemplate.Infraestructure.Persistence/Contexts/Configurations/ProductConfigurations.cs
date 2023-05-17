using Azure;
using JDMarketSLn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Infraestructure.Persistence.Contexts.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {

       
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).HasColumnType("varchar(max)");

            builder
            .HasMany(p => p.SubCategories)
            .WithMany(p => p.Products)
            .UsingEntity<Dictionary<string, object>>(
                "ProductSubCategories",
                j => j
                    .HasOne<SubCategory>()
                    .WithMany()
                    .HasForeignKey("SubCategoryId")
                    .HasConstraintName("FK_ProductSubCategories_SubCategory_SubCategoryId")
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .HasConstraintName("FK_ProductSubCategories_Product_ProductId")
                    .OnDelete(DeleteBehavior.NoAction));

            //builder.HasMany(e => e.SubCategories).WithMany(e => e.Products)
            //.UsingEntity(
            //    "ProductSubCategories",
            //    l => l.HasOne(typeof(Product)).WithMany().HasForeignKey("ProductId").HasPrincipalKey(nameof(Product.Id)),
            //    r => r.HasOne(typeof(SubCategory)).WithMany().HasForeignKey("SubCategoryId").HasPrincipalKey(nameof(SubCategory.Id)),
            //    j => j.HasKey("ProductId", "SubCategoryId")
            //);

            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
        }
    }
}
