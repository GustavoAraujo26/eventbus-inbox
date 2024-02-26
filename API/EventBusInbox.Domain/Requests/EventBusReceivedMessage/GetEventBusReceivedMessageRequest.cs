using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Requests.EventBusReceivedMessage
{
    /// <summary>
    /// Requisição de busca de mensagem recebida no barramento de eventos
    /// </summary>
    public class GetEventBusReceivedMessageRequest : IRequest<AppResponse<GetEventBusReceivedMessageResponse>>
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public GetEventBusReceivedMessageRequest() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="requestId">Identificador da requisição</param>
        public GetEventBusReceivedMessageRequest(Guid requestId)
        {
            RequestId = requestId;
        }

        /// <summary>
        /// Identificador da requisição
        /// </summary>
        public Guid RequestId { get; set; }
    }
}
