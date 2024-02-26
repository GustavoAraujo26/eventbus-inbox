using EventBusInbox.Shared.Enums;
using MediatR;

namespace EventBusInbox.Domain.Notifications
{
    /// <summary>
    /// Notificação para logs da aplicação
    /// </summary>
    public class EventLogNotification : INotification
    {
        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="type">Tipo</param>
        /// <param name="originClass">Classe de origem</param>
        /// <param name="message">Mensagem</param>
        /// <param name="exception">Erro ocorrido</param>
        private EventLogNotification(EventLogNotificationType type, string originClass,
            string message, Exception exception = null)
        {
            Type = type;
            OriginClass = originClass;
            Message = message;
            Exception = exception;
        }

        /// <summary>
        /// Tipo
        /// </summary>
        public EventLogNotificationType Type { get; private set; }

        /// <summary>
        /// Classe de origem
        /// </summary>
        public string OriginClass { get; private set; }

        /// <summary>
        /// Mensagem
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Erro ocorrido
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Cria uma nova notificação da aplicação customizada
        /// </summary>
        /// <typeparam name="T">Tipo da classe que originou a notificação</typeparam>
        /// <param name="currentClass">Classe que originou a notificação</param>
        /// <param name="message">Mensagem</param>
        /// <param name="type">Tipo da notificação</param>
        /// <returns></returns>
        public static EventLogNotification Create<T>(T currentClass, string message,
            EventLogNotificationType type = EventLogNotificationType.Information) where T : class =>
            new EventLogNotification(type, currentClass.GetType().Name, message);

        /// <summary>
        /// Cria uma nova notificação de erro da aplicação
        /// </summary>
        /// <typeparam name="T">Tipo da classe que originou a notificação</typeparam>
        /// <param name="currentClass">Classe que originou a notificação</param>
        /// <param name="ex">Erro</param>
        /// <param name="message">Mensagem</param>
        /// <returns></returns>
        public static EventLogNotification Create<T>(T currentClass, Exception ex, string? message = null) where T : class =>
            new EventLogNotification(EventLogNotificationType.Error, currentClass.GetType().Name, 
                string.IsNullOrEmpty(message) ? ex.Message : message, ex);
    }
}
