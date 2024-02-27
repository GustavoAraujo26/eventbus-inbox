using EventBusInbox.Domain.Responses;
using EventBusInbox.Domain.Validations;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Requests
{
    /// <summary>
    /// Requisição para envio de mensagem no RabbitMQ
    /// </summary>
    public class SendMessageRequest : IRequest<AppResponse<AppTaskResponse>>
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public SendMessageRequest() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="createdAt"></param>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <param name="queueId"></param>
        public SendMessageRequest(Guid requestId, DateTime createdAt, string type, 
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
        public AppResponse<SendMessageRequest> Validate() =>
            AppResponse<SendMessageRequest>.ValidationResponse(new SendMessageValidation().Validate(this));
    }
}
