using EventBusInbox.Shared.Exceptions;
using EventBusInbox.Shared.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace EventBusInbox.Shared.Extensions
{
    /// <summary>
    /// Extensões para logs
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// Configura o Serilog na aplicação
        /// </summary>
        /// <param name="builder">Construtor da aplicação</param>
        /// <exception cref="EnvSettingsNotFoundException">Exceção para configuração de ambiente não encontrada</exception>
        public static void ConfigureAppSerilog(this WebApplicationBuilder builder)
        {
            var logger = CreateAppLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
            builder.Host.UseSerilog(logger);
        }

        /// <summary>
        /// Obtem a interface do logger do Serilog através da coleção de serviços
        /// </summary>
        /// <param name="services">Interface da coleção de serviços</param>
        /// <returns></returns>
        public static Serilog.ILogger GetLogger(this IServiceCollection services) =>
            services.GetService<Serilog.ILogger>();

        /// <summary>
        /// Cria uma nova instância do logger do Serilog
        /// </summary>
        /// <returns></returns>
        public static Serilog.ILogger CreateAppLogger()
        {
            var envSettings = EnvironmentSettings.Instance;

            var newLogger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                //.MinimumLevel.Override("System", LogEventLevel.Information)
                .WriteTo.MongoDB(envSettings.GetMongoDbDatabaseUrl(), "LogsInboxAPI")
                .WriteTo.Console()
                .CreateLogger();

            return newLogger;
        }
    }
}
