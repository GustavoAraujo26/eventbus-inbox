using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Requests.EventBusQueues
{
    /// <summary>
    /// Requisição de atualização de status de fila do barramento de eventos
    /// </summary>
    public class UpdateEventBusQueueStatusRequest : IRequest<AppResponse<EventBusQueueResponse>>
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public UpdateEventBusQueueStatusRequest() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <param name="status">Status</param>
        public UpdateEventBusQueueStatusRequest(Guid id, QueueStatus status)
        {
            Id = id;
            Status = status;
        }

        /// <summary>
        /// Identificador
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public QueueStatus Status { get; set; }
    }
}
