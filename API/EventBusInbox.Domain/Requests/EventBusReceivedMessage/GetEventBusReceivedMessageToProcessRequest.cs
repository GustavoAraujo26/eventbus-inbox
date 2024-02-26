using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Domain.Validations.EventBusReceivedMessage;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Requests.EventBusReceivedMessage
{
    /// <summary>
    /// Requisição para listagem de mensagens recebidas do barramento de eventos para processamento
    /// </summary>
    public class GetEventBusReceivedMessageToProcessRequest : IRequest<AppResponse<GetEventBusReceivedMessageToProcessResponse>>
    {
        /// <summary>
        /// Identificador da fila
        /// </summary>
        public Guid QueueId { get; set; }

        /// <summary>
        /// Realiza validação das informações
        /// </summary>
        /// <returns></returns>
        public AppResponse<GetEventBusReceivedMessageToProcessRequest> Validate() =>
            AppResponse<GetEventBusReceivedMessageToProcessRequest>.ValidationResponse(new GetEventBusReceivedMessageToProcessValidation().Validate(this));
    }
}
