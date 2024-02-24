using EventBusInbox.Domain.Handlers.EventBusQueue;
using EventBusInbox.Handlers.Contracts.EventBusQueue;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventBusInbox.Handlers.Extensions
{
    /// <summary>
    /// Extensões de manipuladores
    /// </summary>
    public static class HandlersExtensions
    {
        /// <summary>
        /// Realiza a injeção de dependência do Mediatr
        /// </summary>
        /// <param name="services">Interface do Service Collection</param>
        public static void ConfigureAppMediator(this IServiceCollection services)
        {
            services.AddTransient<IDeleteEventBusQueueHandler, DeleteEventBusQueueHandler>();
            services.AddTransient<IGetEventBusQueueHandler, GetEventBusQueueHandler>();
            services.AddTransient<ISaveEventBusQueueHandler, SaveEventBusQueueHandler>();
            services.AddTransient<IUpdateEventBusQueueStatusHandler, UpdateEventBusQueueStatusHandler>();

            var configuration = new MediatRServiceConfiguration();
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(configuration);
        }
    }
}
