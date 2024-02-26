using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Domain.Validations.EventBusQueue;
using EventBusInbox.Shared.Models;
using MediatR;

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
        public QueueStatus Status { get; set; }

        /// <summary>
        /// Quantidade de tentativas de processamento
        /// </summary>
        public int ProcessingAttempts { get; set; }

        /// <summary>
        /// Realiza validação das informações
        /// </summary>
        /// <returns></returns>
        public AppResponse<SaveEventBusQueueRequest> Validate() =>
            AppResponse<SaveEventBusQueueRequest>.ValidationResponse(new SaveEventBusQueueValidation().Validate(this));
    }
}
