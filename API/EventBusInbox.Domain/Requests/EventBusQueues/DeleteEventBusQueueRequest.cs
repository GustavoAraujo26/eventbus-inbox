using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Domain.Validations.EventBusQueue;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Requests.EventBusQueues
{
    /// <summary>
    /// Requisição de deleção de fila do barramento de eventos
    /// </summary>
    public class DeleteEventBusQueueRequest : IRequest<AppResponse<EventBusQueueResponse>>
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public DeleteEventBusQueueRequest() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="id"></param>
        public DeleteEventBusQueueRequest(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Identificador
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Realiza validação das informações
        /// </summary>
        /// <returns></returns>
        public AppResponse<DeleteEventBusQueueRequest> Validate() =>
            AppResponse<DeleteEventBusQueueRequest>.ValidationResponse(new DeleteEventBusQueueValidation().Validate(this));
    }
}
