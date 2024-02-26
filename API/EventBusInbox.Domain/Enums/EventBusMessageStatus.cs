using EventBusInbox.Shared.Extensions;
using System.ComponentModel;
using System.Net;

namespace EventBusInbox.Domain.Enums
{
    /// <summary>
    /// Status de uma mensagem do barramento de eventos
    /// </summary>
    public enum EventBusMessageStatus
    {
        /// <summary>
        /// Pendente
        /// </summary>
        [Description("Pending")]
        Pending = 1,
        /// <summary>
        /// Completada
        /// </summary>
        [Description("Completed")]
        Completed = 2,
        /// <summary>
        /// Falha temporária
        /// </summary>
        [Description("Temporary Failure")]
        TemporaryFailure = 3,
        /// <summary>
        /// Falha permanente
        /// </summary>
        [Description("Permanent Failure")]
        PermanentFailure = 4
    }

    /// <summary>
    /// Extensões para o status de mensagem do barramento de eventos
    /// </summary>
    public static class EventBusMessageStatusExtensions
    {
        /// <summary>
        /// Converte HTTP status code para status de mensagem do barramento de eventos
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static EventBusMessageStatus ToMessageStatus(this HttpStatusCode statusCode)
        {
            if (statusCode.IsTemporaryFailure())
                return EventBusMessageStatus.TemporaryFailure;
            else if (statusCode.IsPermanentFailure())
                return EventBusMessageStatus.PermanentFailure;
            else if (statusCode.IsSuccess())
                return EventBusMessageStatus.Completed;
            else
                return EventBusMessageStatus.Pending;
        }
    }
}
