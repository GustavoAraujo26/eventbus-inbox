using EventBusInbox.Domain.Notifications;
using MediatR;

namespace EventBusInbox.Domain.Handlers
{
    /// <summary>
    /// Interface do manipulador de logs de evento
    /// </summary>
    public interface IEventLogHandler : INotificationHandler<EventLogNotification>
    {
    }
}
