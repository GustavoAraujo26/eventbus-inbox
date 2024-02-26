using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Requests.EventBusReceivedMessage
{
    /// <summary>
    /// Requisição de listagem de mensagens recebidas do barramento de eventos
    /// </summary>
    public class GetEventBusReceivedMessageListRequest : IRequest<AppResponse<GetEventBusReceivedMessageListResponse>>
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public GetEventBusReceivedMessageListRequest() { }

        /// <summary>
        /// Identificador da fila
        /// </summary>
        public Guid? QueueId { get; set; }

        /// <summary>
        /// Período de pesquisa sobre a data de criação
        /// </summary>
        public Period CreationDateSearch { get; set; }

        /// <summary>
        /// Período de pesquisa sobre a data da última atualização
        /// </summary>
        public Period UpdateDateSearch { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public IList<EventBusMessageStatus>? StatusToSearch { get; set; }

        /// <summary>
        /// Trecho do tipo da mensagem para pesquisa
        /// </summary>
        public string? TypeMatch { get; set; }

        /// <summary>
        /// Página
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Tamanho da página
        /// </summary>
        public int PageSize { get; set; }
    }
}
