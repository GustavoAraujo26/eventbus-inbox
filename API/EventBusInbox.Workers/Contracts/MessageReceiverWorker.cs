using EventBusInbox.Domain.Notifications;
using EventBusInbox.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventBusInbox.Workers.Contracts
{
    internal class MessageReceiverWorker : BackgroundService
    {
        private readonly IEventBusQueueRepository queueRepository;
        private readonly IRabbitMqRepository rabbitMqRepository;
        private readonly IMediator mediator;

        public MessageReceiverWorker(IServiceProvider serviceProvider)
        {
            queueRepository = serviceProvider.GetRequiredService<IEventBusQueueRepository>();
            rabbitMqRepository = serviceProvider.GetRequiredService<IRabbitMqRepository>();
            mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using (var timer = new PeriodicTimer(TimeSpan.FromMinutes(1)))
                {
                    while (await timer.WaitForNextTickAsync(stoppingToken))
                    {
                        var queueList = await queueRepository.GetActiveList();
                        if (!queueList.Any())
                            continue;

                        await rabbitMqRepository.StartConsumption(queueList, stoppingToken);
                    }
                }
            }
            catch(Exception ex)
            {
                await mediator.Publish(EventLogNotification.Create(this, ex,
                    $"An error occurred on message receiver worker!"));
            }
        }
    }
}
