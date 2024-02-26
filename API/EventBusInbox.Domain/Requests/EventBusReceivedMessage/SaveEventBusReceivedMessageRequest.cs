using EventBusInbox.Domain.Responses;
using EventBusInbox.Domain.Validations.EventBusReceivedMessage;
using EventBusInbox.Shared.Models;
using MediatR;
using Newtonsoft.Json.Linq;

namespace EventBusInbox.Domain.Requests.EventBusReceivedMessage
{
    /// <summary>
    /// Requisição para persistência de mensagem recebida do barramento de eventos
    /// </summary>
    public class SaveEventBusReceivedMessageRequest : IRequest<AppResponse<AppTaskResponse>>
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public SaveEventBusReceivedMessageRequest() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="requestId">Identificador da requisição</param>
        /// <param name="createdAt">Data de criação</param>
        /// <param name="type">Tipo</param>
        /// <param name="content">Conteudo da mensagem</param>
        /// <param name="queueId">Identificador da fila</param>
        public SaveEventBusReceivedMessageRequest(Guid requestId, DateTime createdAt, string type, 
            dynamic content, Guid queueId)
        {
            RequestId = requestId;
            CreatedAt = createdAt;
            Type = type;
            Content = content;
            QueueId = queueId;
        }

        /// <summary>
        /// Identificador da requisição
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Tipo
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Conteudo da mensagem
        /// </summary>
        public dynamic Content { get; set; }

        /// <summary>
        /// Identificador da fila
        /// </summary>
        public Guid QueueId { get; set; }

        /// <summary>
        /// Realiza validação das informações
        /// </summary>
        /// <returns></returns>
        public AppResponse<SaveEventBusReceivedMessageRequest> Validate() =>
            AppResponse<SaveEventBusReceivedMessageRequest>.ValidationResponse(new SaveEventBusReceivedMessageValidation().Validate(this));
    }
}
