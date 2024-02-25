using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Shared.Models;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventBusInbox.Domain.Requests.EventBusQueues
{
    /// <summary>
    /// Requisição de persistência de fila do barramento de eventos
    /// </summary>
    public class SaveEventBusQueueRequest : IRequest<AppResponse<EventBusQueueResponse>>
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public SaveEventBusQueueRequest() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <param name="name">Name</param>
        /// <param name="description">Descrição</param>
        /// <param name="status">Status</param>
        /// <param name="processingAttempts">Quantidade de tentativas de processamento</param>
        public SaveEventBusQueueRequest(Guid id, string name, string description, 
            QueueStatus status, int processingAttempts)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status;
            ProcessingAttempts = processingAttempts;
        }

        /// <summary>
        /// Identificador
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name
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
