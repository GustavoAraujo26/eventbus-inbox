using EventBusInbox.Domain.Handlers;
using EventBusInbox.Domain.Notifications;
using EventBusInbox.Shared.Enums;
using EventBusInbox.Shared.Extensions;
using Serilog;

namespace EventBusInbox.Handlers.Contracts
{
    public class EventLogHandler : IEventLogHandler
    {
        private readonly ILogger logger;

        public EventLogHandler(IServiceProvider serviceProvider)
        {
            logger = serviceProvider.GetAppService<ILogger>();
        }

        public Task Handle(EventLogNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                switch (notification.Type)
                {
                    case EventLogNotificationType.Error:
                        LogErrorEvent(notification);
                        break;
                    default:
                        LogInformationEvent(notification);
                        break;
                }
            });
        }

        private void LogInformationEvent(EventLogNotification notification) =>
            logger.Information($"[{notification.OriginClass}] {notification.Message}");

        private void LogErrorEvent(EventLogNotification notification)
        {
            if (!string.IsNullOrEmpty(notification.Message))
                logger.Error(notification.Exception, $"[{notification.OriginClass}] {notification.Message}");
            else
                logger.Error(notification.Exception, $"[{notification.OriginClass}] An error occurred on executing the request!");
        }
    }
}
