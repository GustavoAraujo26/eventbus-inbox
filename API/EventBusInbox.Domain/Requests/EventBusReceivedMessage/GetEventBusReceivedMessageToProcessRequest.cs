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
        /// Construtor vazio
        /// </summary>
        public GetEventBusReceivedMessageToProcessRequest() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="queueId">Identificador da fila</param>
        /// <param name="page">Página</param>
        /// <param name="pageSize">Tamanho da página</param>
        public GetEventBusReceivedMessageToProcessRequest(Guid queueId, int page, int pageSize)
        {
            QueueId = queueId;
            Page = page;
            PageSize = pageSize;
        }

        /// <summary>
        /// Identificador da fila
        /// </summary>
        public Guid QueueId { get; set; }

        /// <summary>
        /// Página
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Tamanho da página
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Realiza validação das informações
        /// </summary>
        /// <returns></returns>
        public AppResponse<GetEventBusReceivedMessageToProcessRequest> Validate() =>
            AppResponse<GetEventBusReceivedMessageToProcessRequest>.ValidationResponse(new GetEventBusReceivedMessageToProcessValidation().Validate(this));
    }
}
