using EventBusInbox.Domain.Responses.EventBusQueues;
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
        public GetEventBusQueueRequest(Guid? id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Identificador
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Name { get; set; }
    }
}
