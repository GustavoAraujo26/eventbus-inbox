using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Requests.EventBusReceivedMessage
{
    /// <summary>
    /// Requisição de deleção de mensagem recebida do barramento de eventos
    /// </summary>
    public class DeleteEventBusReceivedMessageRequest : IRequest<AppResponse<AppTaskResponse>>
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public DeleteEventBusReceivedMessageRequest() { }

        /// <summary>
        /// Construtor para inicializar a propriedade
        /// </summary>
        /// <param name="requestId">Identificador da requisição</param>
        public DeleteEventBusReceivedMessageRequest(Guid requestId)
        {
            RequestId = requestId;
        }

        /// <summary>
        /// Identificador da requisição
        /// </summary>
        public Guid RequestId { get; set; }
    }
}
