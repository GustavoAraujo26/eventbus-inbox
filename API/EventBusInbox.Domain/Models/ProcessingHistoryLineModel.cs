using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json.Converters;
using System.Net;
using Newtonsoft.Json;

namespace EventBusInbox.Domain.Models
{
    /// <summary>
    /// Modelo do banco de dados para linha do histórico de 
    /// processamento de uma mensagem recebida do barramento de eventos
    /// </summary>
    public class ProcessingHistoryLineModel
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public ProcessingHistoryLineModel() { }

        /// <summary>
        /// Data de ocorrência
        /// </summary>
        public DateTime OccurredAt { get; set; }

        /// <summary>
        /// Status HTTP do resultado do processamento
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public HttpStatusCode ResultStatus { get; set; }

        /// <summary>
        /// Mensagem do resultado do processamento
        /// </summary>
        public string ResultMessage { get; set; }
    }
}
