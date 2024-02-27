using EventBusInbox.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventBusInbox.Workers.Contracts
{
    internal class MessageReceiverWorker : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public MessageReceiverWorker(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using (var scope = serviceScopeFactory.CreateScope())
                    {
                        var queueRepository = scope.ServiceProvider.GetRequiredService<IEventBusQueueRepository>();
                        var rabbitMqRepository = scope.ServiceProvider.GetRequiredService<IRabbitMqRepository>();
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                        var queueList = await queueRepository.GetActiveList();
                        if (!queueList.Any())
                            continue;

                        await rabbitMqRepository.StartConsumption(queueList, stoppingToken);
                    }

                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
