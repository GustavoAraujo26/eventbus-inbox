using Newtonsoft.Json;

namespace EventBusInbox.Shared.Models
{
    /// <summary>
    /// Mensagem do barramento de eventos
    /// </summary>
    public class EventBusMessage
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public EventBusMessage() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="requestId">Identificador da requisição</param>
        /// <param name="createdAt">Data de criação</param>
        /// <param name="type">Tipo da mensagem</param>
        /// <param name="data">Conteúdo da mensagem</param>
        public EventBusMessage(Guid requestId, DateTime createdAt, string type, object data)
        {
            RequestId = requestId;
            CreatedAt = createdAt;
            Type = type;
            Data = data;
        }

        /// <summary>
        /// Identificador da requisição
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Tipo da mensagem
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Conteúdo da mensagem
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// JSON do conteúdo da mensagem
        /// </summary>
        public string DataJson
        {
            get => Convert.ToString(Data);
        }
    }
}
