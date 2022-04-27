using JdMarketSln.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Infrastructure.Persistence.Context.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            // Limit the size of columns to use efficient database types
            builder.Property(p => p.ProductName).IsRequired().HasMaxLength(25);
            builder.Property(p => p.ProductDescription).IsRequired().HasMaxLength(250);

            builder.HasOne(p => p.Category).WithMany().HasForeignKey(c => c.CategoryId).IsRequired();
            builder.HasOne(p => p.Suplier).WithMany().HasForeignKey(c => c.SuplierId).IsRequired();
            //builder.HasMany<Suplier>().WithOne().HasForeignKey(p => p.ProductId).IsRequired();
            //builder.HasMany<Suplier>().WithOne().HasForeignKey(p => p.ProductId).IsRequired();

        }
    }
}

