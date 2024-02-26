using EventBusInbox.Workers.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace EventBusInbox.Workers.Extensions
{
    /// <summary>
    /// Extensões para os workers
    /// </summary>
    public static class WorkersExtensions
    {
        /// <summary>
        /// Configura a injeção de dependência dos serviços hospedados
        /// </summary>
        /// <param name="services">Interface da coleção de serviços</param>
        public static void ConfigureWorkers(this IServiceCollection services)
        {
            services.AddHostedService<MessageReceiverWorker>();
        }
    }
}
