namespace EventBusInbox.Domain.Responses.EventBusQueues
{
    /// <summary>
    /// Resposta de persistência da fila do barramento de eventos
    /// </summary>
    public class EventBusQueueResponse
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public EventBusQueueResponse() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <param name="occurredAt">Data de ocorrência</param>
        public EventBusQueueResponse(Guid id, DateTime occurredAt)
        {
            Id = id;
            OccurredAt = occurredAt;
        }

        /// <summary>
        /// Identificador
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Data de ocorrência
        /// </summary>
        public DateTime OccurredAt { get; set; }
    }
}
