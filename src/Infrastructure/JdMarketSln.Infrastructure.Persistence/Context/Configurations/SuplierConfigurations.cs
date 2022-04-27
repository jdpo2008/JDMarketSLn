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
    public class SuplierConfigurations : IEntityTypeConfiguration<Suplier>
    {
        public void Configure(EntityTypeBuilder<Suplier> builder)
        {
            // Primary key
            builder.HasKey(s => s.Id);

            // Limit the size of columns to use efficient database types
            builder.Property(s => s.BusinessName).IsRequired().HasMaxLength(50);
            builder.Property(s => s.TaxIdentifier).IsRequired().HasMaxLength(15);
            builder.Property(s => s.ContactName).HasMaxLength(150);
            builder.Property(s => s.ContactPhone).HasMaxLength(25);
            builder.Property(s => s.ContactEmail).HasMaxLength(150);
            builder.Property(s => s.Country).HasMaxLength(25);
            builder.Property(s => s.City).HasMaxLength(25);
            builder.Property(s => s.Adrress).HasMaxLength(150);

            builder.HasMany<Product>().WithOne().HasForeignKey(p => p.SuplierId).IsRequired();

        }
    }
}
