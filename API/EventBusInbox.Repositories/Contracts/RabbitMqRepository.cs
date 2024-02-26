using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Repositories;
using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Shared.Models;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBusInbox.Repositories.Contracts
{
    internal class RabbitMqRepository : IRabbitMqRepository
    {
        private readonly IMediator mediator;
        private readonly EnvironmentSettings envSettings;

        public RabbitMqRepository(IMediator mediator, EnvironmentSettings envSettings)
        {
            this.mediator = mediator;
            this.envSettings = envSettings;
        }

        public async Task SendMessage(EventBusReceivedMessage message)
        {
            var eventBusMessage = new EventBusMessage(message.RequestId, message.CreatedAt, 
                message.Type, message.Content);

            using (var connection = BuildConnection())
            using (var channel = connection.CreateModel())
            {
                var subscription = new RabbitMqSubscription(message.Queue.Name, false);

                channel.BasicPublish(exchange: subscription.Exchange,
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
            using (var connection = BuildConnection())
            {
                List<Task> tasks = new List<Task>();

                queues.ForEach(x => tasks.Add(ConsumeQueue(connection, x, cancellationToken)));

                await Task.WhenAll(tasks);
            }
        }

        private async Task ConsumeQueue(IConnection connection, EventBusQueue queue, CancellationToken cancellationToken)
        {
            string workerExchange = $"worker.{queue.Name}.exchange";
            string workerQueue = $"worker.{queue.Name}.queue";
            string retryExchange = $"retry.{queue.Name}.exchange";
            string retryQueue = $"retry.{queue.Name}.queue";

            using (var channel = connection.CreateModel())
            {
                var activeSubscription = new RabbitMqSubscription(queue.Name, false);
                var deadletterSubscription = new RabbitMqSubscription(queue.Name, true);

                activeSubscription.ConfigureConsumerChannel(channel, deadletterSubscription);
                deadletterSubscription.ConfigureConsumerChannel(channel, activeSubscription);

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += async (ch, ea) => await ReceiveMessage(channel, ch, ea, queue);

                channel.BasicConsume(workerQueue, false, consumer);

                while (!cancellationToken.IsCancellationRequested)
                    await Task.Delay(60000);
            }
        }

        private IConnection BuildConnection()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(envSettings.RabbitMqConnectionString),
                DispatchConsumersAsync = true
            };

            return factory.CreateConnection();
        }

        private async Task ReceiveMessage(IModel channel, object ch, BasicDeliverEventArgs ea, EventBusQueue queue)
        {
            var json = ea.Body.ToString();
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
        }
    }
}
