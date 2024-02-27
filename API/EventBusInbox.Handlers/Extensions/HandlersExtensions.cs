using EventBusInbox.Domain.Handlers;
using EventBusInbox.Domain.Handlers.EventBusQueue;
using EventBusInbox.Domain.Handlers.EventBusReceivedMessage;
using EventBusInbox.Handlers.Contracts;
using EventBusInbox.Handlers.Contracts.EventBusQueue;
using EventBusInbox.Handlers.Contracts.EventBusReceivedMessage;
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
            services.AddTransient<IEventLogHandler, EventLogHandler>();
            
            services.AddTransient<IDeleteEventBusQueueHandler, DeleteEventBusQueueHandler>();
            services.AddTransient<IGetEventBusQueueHandler, GetEventBusQueueHandler>();
            services.AddTransient<IGetEventBusQueueListHandler, GetEventBusQueueListHandler>();
            services.AddTransient<ISaveEventBusQueueHandler, SaveEventBusQueueHandler>();
            services.AddTransient<IUpdateEventBusQueueStatusHandler, UpdateEventBusQueueStatusHandler>();

            services.AddTransient<IDeleteEventBusReceivedMessageHandler, DeleteEventBusReceivedMessageHandler>();
            services.AddTransient<IGetEventBusReceivedMessageHandler, GetEventBusReceivedMessageHandler>();
            services.AddTransient<IGetEventBusReceivedMessageListHandler, GetEventBusReceivedMessageListHandler>();
            services.AddTransient<IGetEventBusReceivedMessageToProcessHandler, GetEventBusReceivedMessageToProcessHandler>();
            services.AddTransient<IReactivateEventBusReceivedMessageHandler, ReactivateEventBusReceivedMessageHandler>();
            services.AddTransient<ISaveEventBusReceivedMessageHandler, SaveEventBusReceivedMessageHandler>();
            services.AddTransient<IUpdateEventBusReceivedMessageStatusHandler, UpdateEventBusReceivedMessageStatusHandler>();

            services.AddTransient<ISendMessageHandler, SendMessageHandler>();

            var configuration = new MediatRServiceConfiguration();
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(configuration);
        }
    }
}
