using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Repositories.Base;
using EventBusInbox.Domain.Requests.EventBusQueues;
using EventBusInbox.Domain.Responses.EventBusQueues;

namespace EventBusInbox.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de filas do barramento de eventos
    /// </summary>
    public interface IEventBusQueueRepository : IBaseRepository<EventBusQueue>
    {
        /// <summary>
        /// Retorna fila com base no seu nome
        /// </summary>
        /// <param name="name">Nome da fila</param>
        /// <returns></returns>
        Task<EventBusQueue> GetByName(string name);

        /// <summary>
        /// Retorna fila com base em parâmetros de pesquisa informados
        /// </summary>
        /// <param name="request">Parâmetros de pesquisa</param>
        /// <returns></returns>
        Task<GetEventBusQueueResponse> Get(GetEventBusQueueRequest request);

        /// <summary>
        /// Retorna lista de filas com base em parâmetros de pesquisa informados
        /// </summary>
        /// <param name="request">Parâmetros de pesquisa</param>
        /// <returns></returns>
        Task<List<GetEventBusQueueResponse>> List(GetEventBusQueueListRequest request);
    }
}
