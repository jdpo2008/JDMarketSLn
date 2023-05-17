using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using JDMarketSLn.Application.Common.Interfaces;
using JDMarketSLn.Domain.Entities;
using JDMarketSLn.Infraestructure.Persistence.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Infraestructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        private readonly IMediator _mediator;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
        {
            _mediator = mediator;
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        public DbSet<Log> Logs => Set<Log>();

        public DbSet<Product> Products => Set<Product>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<SubCategory> SubCategories => Set<SubCategory>();

        public DbSet<ProductDetail> ProductDetails => Set<ProductDetail>();

        public DbSet<UnitMeasureProduct> UnitMeasureProducts => Set<UnitMeasureProduct>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }

            //foreach (var entityType in builder.Model.GetEntityTypes())
            //{
            //    // 1. Add the IsDeleted property
            //    entityType.AddProperty("IsDeleted", typeof(bool));

            //    // 2. Create the query filter
            //    var parameter = Expression.Parameter(entityType.ClrType);

            //    // EF.Property<bool>(post, "IsDeleted")
            //    var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
            //    var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));

            //    // EF.Property<bool>(post, "IsDeleted") == false
            //    BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));

            //    // post => EF.Property<bool>(post, "IsDeleted") == false
            //    var lambda = Expression.Lambda(compareExpression, parameter);

            //    builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            //}

            //builder.Entity<Product>().Property<bool>("IsDeleted");
            //builder.Entity<Product>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
