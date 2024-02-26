namespace EventBusInbox.Domain.Responses
{
    /// <summary>
    /// Resposta de persistência da fila do barramento de eventos
    /// </summary>
    public class AppTaskResponse
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public AppTaskResponse() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <param name="occurredAt">Data de ocorrência</param>
        public AppTaskResponse(Guid id, DateTime? occurredAt = null)
        {
            Id = id;

            if (occurredAt.HasValue)
                OccurredAt = occurredAt.Value;
            else
                OccurredAt = DateTime.Now;
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
