using EventBusInbox.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventBusInbox.Domain.Responses.EventBusQueues
{
    /// <summary>
    /// Resposta de pesquisa de fila de barramento de eventos
    /// </summary>
    public class GetEventBusQueueResponse
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public QueueStatus Status { get; set; }

        /// <summary>
        /// Quantidade de tentativas de processamento
        /// </summary>
        public int ProcessingAttempts { get; set; }
    }
}
