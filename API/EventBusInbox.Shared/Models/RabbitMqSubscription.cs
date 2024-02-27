using RabbitMQ.Client;
using System.Text.RegularExpressions;

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
            string treatedQueueName = TreatQueueName(queueName);

            string sufix = "active";
            if (isDeadletter)
                sufix = "deadletter";

            Exchange = $"{treatedQueueName}.{sufix}.exchange";
            Queue = $"{treatedQueueName}.{sufix}.queue";
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
        public void ConfigureConsumerChannel(IModel channel)
        {
            channel.QueueDeclare(queue: Queue,
                durable: true,
                exclusive: false,
                autoDelete: false);
        }

        /// <summary>
        /// Configure canal para envio de mensagens
        /// </summary>
        /// <param name="channel">Canal</param>
        public void ConfigureSenderChannel(IModel channel)
        {
            channel.QueueDeclare(queue: Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                null);
        }

        private string TreatQueueName(string queueName)
        {
            string withoutSpecialCharecters = Regex.Replace(queueName, "[^0-9A-Za-z _-]", "").ToLower();
            string withoutWhiteSpace = Regex.Replace(withoutSpecialCharecters, @"\s+", "");

            return withoutWhiteSpace.ToLowerInvariant();
        }
    }
}
