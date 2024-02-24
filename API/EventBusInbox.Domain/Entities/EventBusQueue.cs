using EventBusInbox.Domain.Enums;
using Newtonsoft.Json;

namespace EventBusInbox.Domain.Entities
{
    /// <summary>
    /// Entidade que representa a fila do barramento de eventos
    /// </summary>
    public class EventBusQueue
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public EventBusQueue() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <param name="name">Nome</param>
        /// <param name="description">Descrição</param>
        /// <param name="status">Status</param>
        /// <param name="processingAttempts">Quantidade de tentativas de processamento</param>
        private EventBusQueue(Guid id, string name, string description, QueueStatus status, int processingAttempts)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status;
            ProcessingAttempts = processingAttempts;

        }

        /// <summary>
        /// Identificador
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Status
        /// </summary>
        public QueueStatus Status { get; private set; }

        /// <summary>
        /// Quantidade de tentativas de processamento
        /// </summary>
        public int ProcessingAttempts { get; private set; }

        /// <summary>
        /// Retorna JSON da entidade
        /// </summary>
        /// <returns></returns>
        public override string ToString() => 
            JsonConvert.SerializeObject(this);

        /// <summary>
        /// Cria nova fila
        /// </summary>
        /// <param name="name">Nome</param>
        /// <param name="description">Descrição</param>
        /// <param name="status">Status</param>
        /// <param name="processingAttempts">Quantidade de tentativas de processamento</param>
        /// <returns></returns>
        public static EventBusQueue Create(string name, string description, QueueStatus status, int processingAttempts) =>
            new EventBusQueue(Guid.NewGuid(), name, description, status, processingAttempts);

        /// <summary>
        /// Realiza cópia dos dados
        /// </summary>
        /// <param name="origin">Origem da cópia</param>
        /// <returns></returns>
        public EventBusQueue Copy() =>
            JsonConvert.DeserializeObject<EventBusQueue>(this.ToString());

        /// <summary>
        /// Altera o status da fila para habilitado
        /// </summary>
        public void Enable() => Status = QueueStatus.Enabled;

        /// <summary>
        /// Altera o status da fila para desabilitado
        /// </summary>
        public void Disable() => Status = QueueStatus.Disabled;

        /// <summary>
        /// Atualiza dados básicos
        /// </summary>
        /// <param name="name">Nome</param>
        /// <param name="description">Descrição</param>
        public void UpdateBasicData(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
