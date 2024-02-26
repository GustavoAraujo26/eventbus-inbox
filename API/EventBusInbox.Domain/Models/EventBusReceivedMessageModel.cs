using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using EventBusInbox.Domain.Enums;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace EventBusInbox.Domain.Models
{
    /// <summary>
    /// Modelo do banco de dados para mensagem recebida do barramento de eventos
    /// </summary>
    public class EventBusReceivedMessageModel
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public EventBusReceivedMessageModel() { }

        /// <summary>
        /// Identificador da requisição
        /// </summary>
        [BsonId]
        public Guid RequestId { get; set; }

        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Tipo da mensagem
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Conteudo da mensagem
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// Fila na qual a mensagem foi recebida
        /// </summary>
        public EventBusQueueModel Queue { get; set; }

        /// <summary>
        /// Status da mensagem
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public EventBusMessageStatus Status { get; set; }

        /// <summary>
        /// Quantidade de processamentos
        /// </summary>
        public int ProcessingAttempts { get; set; }

        /// <summary>
        /// Histórico de processamento da mensagem
        /// </summary>
        public IList<ProcessingHistoryLineModel> ProcessingHistory { get; set; }
    }
}
