using Ardalis.EFCore.Extensions;
using JdMarketSln.Application.Interfaces;
using JdMarketSln.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JdMarketSln.Infrastructure.Identity.Contexts
{
    public class JDMarketIdentityDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin,
        RoleClaim, UserToken>
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;
        public JDMarketIdentityDbContext(DbContextOptions<JDMarketIdentityDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser; 
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<User>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        entry.Entity.CreatedAt = _dateTime.NowUtc;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateAt = _dateTime.NowUtc;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<Role>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        entry.Entity.CreatedAt = _dateTime.NowUtc;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateAt = _dateTime.NowUtc;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

            modelBuilder.Entity<User>().Property<bool>("IsDeleted");
            modelBuilder.Entity<User>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

            modelBuilder.Entity<Role>().Property<bool>("IsDeleted");
            modelBuilder.Entity<Role>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

            modelBuilder.HasDefaultSchema("IDENTITY");

            modelBuilder.Entity<User>().ToTable("Users", "IDENTITY");

            modelBuilder.Entity<UserClaim>().ToTable("UserClaims", "IDENTITY");

            modelBuilder.Entity<UserLogin>().ToTable("UserLogins", "IDENTITY");

            modelBuilder.Entity<UserToken>().ToTable("UserTokens", "IDENTITY");

            modelBuilder.Entity<Role>().ToTable("Roles", "IDENTITY");

            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", "IDENTITY");

            modelBuilder.Entity<UserRole>().ToTable("UserRoles", "IDENTITY");

        }
    }
}
