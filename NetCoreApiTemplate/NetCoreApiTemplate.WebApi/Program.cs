using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using JDMarketSLn.Infraestructure.Persistence;
using JDMarketSLn.WebApi.Middlewares;
using NLog;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddApplicationServices();
    builder.Services.AddPersistenceInfrastructureServices(builder.Configuration, builder.Environment);
    builder.Services.AddIdentityInfrastructureServices(builder.Configuration, builder.Environment);
    builder.Services.AddSharedInfrastructureServices(builder.Configuration);
    builder.Services.AddWebUIServices();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "CorsPolicy", builder =>
        {
            builder.SetIsOriginAllowed(origins => true)
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
        });

    });

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            //c.SwaggerEndpoint("/swagger/v1/swagger.json", "JDMarketSLn.WebApi");
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    $"JDMarketSLn.WebApi {description.GroupName.ToUpperInvariant()}");
            }
            c.DefaultModelExpandDepth(2);
            c.DefaultModelRendering(ModelRendering.Model);
            c.DocExpansion(DocExpansion.None);
            c.EnableDeepLinking();
            c.DisplayOperationId();
        });

        app.UseDeveloperExceptionPage();
        app.UseMigrationsEndPoint();

        // Initialise and seed database
        using (var scope = app.Services.CreateScope())
        {
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();

            var initialiserIdentityDb = scope.ServiceProvider.GetRequiredService<IdentityDbContextInitialiser>();
            await initialiserIdentityDb.InitialiseAsync();
            await initialiserIdentityDb.SeedAsync();
        }
    }

    app.UseHealthChecks("/health");
    app.UseHttpsRedirection();

    app.UseRouting();
    app.UseCors("CorsPolicy");
    app.UseStaticFiles();
    app.UseAuthentication();
    app.UseAuthorization();
    //app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.Run();
}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}


