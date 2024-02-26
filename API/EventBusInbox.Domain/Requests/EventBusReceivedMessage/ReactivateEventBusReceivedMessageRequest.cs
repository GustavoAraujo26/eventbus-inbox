using EventBusInbox.Domain.Responses;
using EventBusInbox.Domain.Validations.EventBusReceivedMessage;
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
        /// Construtor vazio
        /// </summary>
        public ReactivateEventBusReceivedMessageRequest() { }

        /// <summary>
        /// Construtor para inicializar a propriedade
        /// </summary>
        /// <param name="requestId">Identificador da requisição</param>
        public ReactivateEventBusReceivedMessageRequest(Guid requestId)
        {
            RequestId = requestId;
        }

        /// <summary>
        /// Identificador da requisição
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Realiza validação das informações
        /// </summary>
        /// <returns></returns>
        public AppResponse<ReactivateEventBusReceivedMessageRequest> Validate() =>
            AppResponse<ReactivateEventBusReceivedMessageRequest>.ValidationResponse(new ReactivateEventBusReceivedMessageValidation().Validate(this));
    }
}
