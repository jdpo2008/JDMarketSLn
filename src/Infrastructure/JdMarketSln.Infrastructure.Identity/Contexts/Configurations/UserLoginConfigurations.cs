using JdMarketSln.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Infrastructure.Identity.Contexts.Configurations
{
    public class UserLoginConfigurations : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            // Primary key
            builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            // Limit the size of columns to use efficient database types
            builder.Property(l => l.LoginProvider).HasMaxLength(125);
            builder.Property(l => l.ProviderKey).HasMaxLength(125);
        }
    }
}
