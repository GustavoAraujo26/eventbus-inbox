using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using MediatR;
using System.Net;

namespace EventBusInbox.Domain.Requests.EventBusReceivedMessage
{
    /// <summary>
    /// Requisição para atualização do status da mensagem recebida do barramento de eventos
    /// </summary>
    public class UpdateEventBusReceivedMessageStatusRequest : IRequest<AppResponse<AppTaskResponse>>
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public UpdateEventBusReceivedMessageStatusRequest() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="requestId">Identificador da requisição</param>
        /// <param name="processStatus">Status do processamento</param>
        /// <param name="resultMessage">Mensagem do resultado do processamento</param>
        public UpdateEventBusReceivedMessageStatusRequest(Guid requestId, HttpStatusCode processStatus, string resultMessage)
        {
            RequestId = requestId;
            ProcessStatus = processStatus;
            ResultMessage = resultMessage;
        }

        /// <summary>
        /// Identificador da requisição
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Status do processamento
        /// </summary>
        public HttpStatusCode ProcessStatus { get; set; }

        /// <summary>
        /// Mensagem do resultado do processamento
        /// </summary>
        public string ResultMessage { get; set; }
    }
}
