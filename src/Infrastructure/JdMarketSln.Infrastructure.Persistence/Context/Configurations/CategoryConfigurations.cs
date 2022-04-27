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
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            // Limit the size of columns to use efficient database types
            builder.Property(p => p.CategoryName).IsRequired().HasMaxLength(25);
            builder.Property(p => p.CategoryDescription).IsRequired().HasMaxLength(250);

            builder.HasMany<Product>().WithOne().HasForeignKey(p => p.CategoryId).IsRequired();

        }
    }
}
