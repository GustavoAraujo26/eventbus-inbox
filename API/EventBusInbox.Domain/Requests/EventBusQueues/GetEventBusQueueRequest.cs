using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Domain.Validations.EventBusQueue;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Requests.EventBusQueues
{
    /// <summary>
    /// Requisição de pesquisa de filas do barramento de eventos
    /// </summary>
    public class GetEventBusQueueRequest : IRequest<AppResponse<GetEventBusQueueResponse>>
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public GetEventBusQueueRequest() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <param name="name">Nome</param>
        public GetEventBusQueueRequest(Guid? id, string name, bool summarizeMessages)
        {
            Id = id;
            Name = name;
            SummarizeMessages = summarizeMessages;
        }

        /// <summary>
        /// Identificador
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Retorna contagem de mensagens?
        /// </summary>
        public bool SummarizeMessages { get; set; }

        /// <summary>
        /// Realiza validação das informações
        /// </summary>
        /// <returns></returns>
        public AppResponse<GetEventBusQueueRequest> Validate() =>
            AppResponse<GetEventBusQueueRequest>.ValidationResponse(new GetEventBusQueueValidation().Validate(this));
    }
}
