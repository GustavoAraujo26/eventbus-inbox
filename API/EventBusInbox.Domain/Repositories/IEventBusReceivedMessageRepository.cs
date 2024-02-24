using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Repositories.Base;

namespace EventBusInbox.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de mensagens recebidas do barramento de eventos
    /// </summary>
    public interface IEventBusReceivedMessageRepository : IBaseRepository<EventBusReceivedMessage>
    { 
    }
}
