using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Requests.EventBusReceivedMessage
{
    /// <summary>
    /// Requisição de reativação de mensagem recebida no barramento de eventos
    /// </summary>
    public class ReactivateEventBusReceivedMessageRequest : IRequest<AppResponse<AppTaskResponse>>
    {
        /// <summary>
        /// Id da requisição
        /// </summary>
        public Guid RequestId { get; set; }
    }
}
