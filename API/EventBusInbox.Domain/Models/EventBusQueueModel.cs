using EventBusInbox.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventBusInbox.Domain.Models
{
    /// <summary>
    /// Modelo do banco de dados para filas do barramento de eventos
    /// </summary>
    public class EventBusQueueModel
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public EventBusQueueModel() { }

        /// <summary>
        /// Identificador
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
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
        [BsonRepresentation(BsonType.String)]
        public QueueStatus Status { get; set; }

        /// <summary>
        /// Quantidade de tentativas de processamento
        /// </summary>
        public int ProcessingAttempts { get; set; }
    }
}
