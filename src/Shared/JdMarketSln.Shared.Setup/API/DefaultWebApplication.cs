﻿namespace JdMarketSln.Shared.Setup.API
{
    public static class DefaultWebApplication
    {
        public static WebApplication Create(string[] args, Action<WebApplicationBuilder>? webappBuilder = null)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Host.ConfigureSerilog();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRouting(x => x.LowercaseUrls = true);
            builder.Services.AddSerializer();

            builder.Services.AddServiceDiscovery(builder.Configuration);
            builder.Services.AddSecretManager(builder.Configuration);
            builder.Services.AddLogging(logger => logger.AddSerilog());

            if (webappBuilder != null)
            {
                webappBuilder.Invoke(builder);
            }

            return builder.Build();
        }

        public static void Run(WebApplication webApp)
        {
            if (webApp.Environment.IsDevelopment())
            {
                webApp.UseSwagger();
                webApp.UseSwaggerUI();
            }


            webApp.UseHttpsRedirection();
            webApp.UseAuthorization();
            webApp.MapControllers();
            webApp.Run();
        }
    }
}
