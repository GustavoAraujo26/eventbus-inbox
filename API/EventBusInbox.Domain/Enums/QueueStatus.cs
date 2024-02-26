using System.ComponentModel;

namespace EventBusInbox.Domain.Enums
{
    /// <summary>
    /// Status da fila do barramento de eventos
    /// </summary>
    public enum QueueStatus
    {
        /// <summary>
        /// Habilitado
        /// </summary>
        [Description("Enabled")]
        Enabled = 1,
        /// <summary>
        /// Desabilitado
        /// </summary>
        [Description("Disabled")]
        Disabled = 2
    }
}
