using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Repositories.Base;

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
        /// <param name="name"></param>
        /// <returns></returns>
        Task<EventBusQueue> GetByName(string name);
    }
}
