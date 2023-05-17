using JDMarketSLn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Infraestructure.Persistence.Contexts.Configurations
{
    public class SubCategoryConfigurations : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).HasColumnType("varchar(max)");

            builder.HasOne<Category>(s => s.Category).WithMany(g => g.SubCategories).HasForeignKey(s => s.CategoryId).OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
        }
    }
}
