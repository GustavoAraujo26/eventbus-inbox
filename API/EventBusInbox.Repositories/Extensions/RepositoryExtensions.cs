using EventBusInbox.Domain.Repositories;
using EventBusInbox.Repositories.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace EventBusInbox.Repositories.Extensions
{
    /// <summary>
    /// Extensões para repositório
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Configura a injeção de dependência dos repositórios na aplicação
        /// </summary>
        /// <param name="services">Interface do Service Collection</param>
        public static void ConfigureAppRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEventBusQueueRepository, EventBusQueueRepository>();
            services.AddScoped<IEventBusReceivedMessageRepository, EventBusReceivedMessageRepository>();
        }
    }
}
