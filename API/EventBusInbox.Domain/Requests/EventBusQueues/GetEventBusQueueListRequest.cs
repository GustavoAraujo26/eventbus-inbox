using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Domain.Validations.EventBusQueue;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Requests.EventBusQueues
{
    /// <summary>
    /// Requisição de listagem de filas do barramento de eventos
    /// </summary>
    public class GetEventBusQueueListRequest : IRequest<AppResponse<GetEventBusQueueResponse>>
    {
        /// <summary>
        /// Trecho do nome para pesquisa
        /// </summary>
        public string? NameMatch { get; set; }

        /// <summary>
        /// Trecho da descrição para pesquisa
        /// </summary>
        public string? DescriptionMatch { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public QueueStatus? Status { get; set; }

        /// <summary>
        /// Número da página
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
        public AppResponse<GetEventBusQueueListRequest> Validate() =>
            AppResponse<GetEventBusQueueListRequest>.ValidationResponse(new GetEventBusQueueListValidation().Validate(this));

    }
}
