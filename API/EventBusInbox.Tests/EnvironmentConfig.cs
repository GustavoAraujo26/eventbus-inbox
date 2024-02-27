using Microsoft.Extensions.DependencyInjection;
using EventBusInbox.TypeConverters.Extensions;
using EventBusInbox.Handlers.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace EventBusInbox.Tests
{
    internal static class EnvironmentConfig
    {
        public static IServiceCollection BuildServices()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.ConfigureAppAutoMapper();
            services.ConfigureAppMediator();

            var newLogger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .WriteTo.Console()
                .CreateLogger();

            services.AddLogging(x =>
            {
                x.ClearProviders();
                x.AddSerilog(newLogger, dispose: true);
            });

            services.AddSingleton<Serilog.ILogger>(newLogger);

            return services;
        }
    }
}
