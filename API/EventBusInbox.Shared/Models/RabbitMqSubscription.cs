using RabbitMQ.Client;
using System.Collections.Generic;

namespace EventBusInbox.Shared.Models
{
    /// <summary>
    /// Assinatura para o RabbitMQ
    /// </summary>
    public class RabbitMqSubscription
    {
        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="queueName">Nome da fila</param>
        /// <param name="isDeadletter">Fila é uma deadletter?</param>
        public RabbitMqSubscription(string queueName, bool isDeadletter)
        {
            string sufix = "active";
            if (isDeadletter)
                sufix = "deadletter";

            Exchange = $"{queueName}.{sufix}.exchange";
            Queue = $"{queueName}.{sufix}.exchange";
            Type = isDeadletter ? "fanout" : "direct";
            IsDeadletter = isDeadletter;
        }

        /// <summary>
        /// Exchange
        /// </summary>
        public string Exchange { get; private set; }

        /// <summary>
        /// Fila
        /// </summary>
        public string Queue { get; private set; }

        /// <summary>
        /// Tipo
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// É uma deadletter?
        /// </summary>
        public bool IsDeadletter { get; set; }

        /// <summary>
        /// Configura canal para recebimento das mensagens
        /// </summary>
        /// <param name="channel">Canal</param>
        /// <param name="linkedSubscription">Assinatura vinculada</param>
        public void ConfigureConsumerChannel(IModel channel, RabbitMqSubscription linkedSubscription)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", linkedSubscription.Exchange },
                { "x-dead-letter-routing-key", linkedSubscription.Queue },
            };
            if (IsDeadletter)
            {
                dictionary = new Dictionary<string, object>
                {
                    { "x-dead-letter-exchange", linkedSubscription.Exchange },
                    { "x-message-ttl", 30000 },
                };
            }

            channel.ExchangeDeclare(Exchange,
                    type: Type,
                    durable: true,
                    autoDelete: false);

            channel.QueueDeclare(queue: Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                dictionary);

            channel.QueueBind(Queue, Exchange, string.Empty);
        }

        /// <summary>
        /// Configure canal para envio de mensagens
        /// </summary>
        /// <param name="channel">Canal</param>
        public void ConfigureSenderChannel(IModel channel)
        {
            channel.ExchangeDeclare(Exchange,
                    type: Type,
                    durable: true,
                    autoDelete: false);

            channel.QueueDeclare(queue: Queue,
                durable: true,
                exclusive: false,
                autoDelete: false);

            channel.QueueBind(Queue, Exchange, string.Empty);
        }
    }
}
