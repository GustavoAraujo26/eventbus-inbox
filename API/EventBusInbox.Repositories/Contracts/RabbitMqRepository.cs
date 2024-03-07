using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Notifications;
using EventBusInbox.Domain.Repositories;
using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Shared.Models;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EventBusInbox.Repositories.Contracts
{
    internal class RabbitMqRepository : IRabbitMqRepository
    {
        private readonly IMediator mediator;
        private readonly EnvironmentSettings envSettings;
        private DateTime LastActivity;

        public RabbitMqRepository(IMediator mediator, EnvironmentSettings envSettings)
        {
            this.mediator = mediator;
            this.envSettings = envSettings;
            LastActivity = DateTime.Now;
        }

        public void SendMessage(EventBusReceivedMessage message)
        {
            var eventBusMessage = new EventBusMessage(message.RequestId, message.CreatedAt, 
                message.Type, message.Content);

            using (var connection = BuildConnection())
            using (var channel = connection.CreateModel())
            {
                var subscription = new RabbitMqSubscription(message.Queue.Name, false);

                subscription.ConfigureSenderChannel(channel);

                channel.BasicPublish(exchange: string.Empty,
                    routingKey: subscription.Queue, basicProperties: null,
                    body: eventBusMessage.ToBytes());
            }
        }

        /// <summary>
        /// Inicia o consumo das mensagens das filas
        /// </summary>
        /// <param name="queues">Lista de filas do barramento de eventos</param>
        /// <param name="cancellationToken">token para cancelamento</param>
        /// <returns></returns>
        public async Task StartConsumption(List<EventBusQueue> queues, CancellationToken cancellationToken)
        {
            try
            {
                using (var connection = BuildConnection())
                {
                    List<Task> tasks = new List<Task>();

                    queues.ForEach(x => tasks.Add(ConsumeQueue(connection, x, cancellationToken)));
                    
                    await Task.WhenAll(tasks);
                }
            }
            catch(Exception ex)
            {
                await mediator.Publish(EventLogNotification.Create(this, ex,
                    $"An error occurred on RabbitMQ repository!"));
            }
        }

        private async Task ConsumeQueue(IConnection connection, EventBusQueue queue, CancellationToken cancellationToken)
        {
            var consumptionState = new RabbitMqConsumptionState();

            using (var channel = connection.CreateModel())
            {
                var subscription = new RabbitMqSubscription(queue.Name, false);

                subscription.ConfigureConsumerChannel(channel);

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += async (ch, ea) => await ReceiveMessage(channel, ch, ea, queue, consumptionState);

                channel.BasicConsume(subscription.Queue, false, consumer);

                while (!cancellationToken.IsCancellationRequested && !consumptionState.CanFinish)
                    await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }

        private IConnection BuildConnection()
        {
#if DEBUG
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };

            return factory.CreateConnection();
#else
            var factory = new ConnectionFactory
            {
                DispatchConsumersAsync = true,
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            return factory.CreateConnection();
#endif


        }

        private async Task ReceiveMessage(IModel channel, object ch, BasicDeliverEventArgs ea, EventBusQueue queue, RabbitMqConsumptionState consumptionState)
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var message = JsonConvert.DeserializeObject<EventBusMessage>(json);
            if (message is null)
            {
                channel.BasicNack(ea.DeliveryTag, false, true);
                return;
            }

            var saveRequest = new SaveEventBusReceivedMessageRequest(message.RequestId, message.CreatedAt, 
                message.Type, message.Content, queue.Id);
            
            var response = await mediator.Send(saveRequest);
            if (response.IsSuccess)
                channel.BasicAck(ea.DeliveryTag, false);
            else
                channel.BasicNack(ea.DeliveryTag, false, true);

            consumptionState.Update();
        }
    }
}
