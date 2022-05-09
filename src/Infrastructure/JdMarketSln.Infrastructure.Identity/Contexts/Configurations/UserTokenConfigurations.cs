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
    public class UserTokenConfigurations : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            // Primary key
            builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            // Limit the size of columns to use efficient database types
            builder.Property(t => t.LoginProvider).HasMaxLength(100);
            builder.Property(t => t.Name).HasMaxLength(100);
        }
    }
}
