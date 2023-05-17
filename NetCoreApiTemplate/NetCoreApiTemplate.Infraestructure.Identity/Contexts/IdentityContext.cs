using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCoreApiTemplate.Application.Common.Interfaces;
using System.Reflection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using NetCoreApiTemplate.Domain.Entities.Identity;
using MediatR;

namespace NetCoreApiTemplate.Infraestructure.Identity.Contexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly IDateTime _dateTime;
        private readonly IMediator _mediator;

        public IdentityContext(DbContextOptions<IdentityContext> options, IDateTime dateTime, IMediator mediator)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _mediator = mediator;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<ApplicationUser>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        entry.Entity.Created = _dateTime.Now;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }

            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ApplicationUser>().Property<bool>("IsDeleted");
            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

            modelBuilder.HasDefaultSchema("IDENTITY");

            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "IDENTITY");

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", "IDENTITY");

            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", "IDENTITY");

            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", "IDENTITY");

            modelBuilder.Entity<ApplicationRole>().ToTable("Roles", "IDENTITY");

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", "IDENTITY");

            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", "IDENTITY");

        }
    }
}
