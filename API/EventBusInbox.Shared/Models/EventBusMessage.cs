using Newtonsoft.Json;
using System.Text;

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
        /// <param name="requestId">Id da requisição</param>
        /// <param name="createdAt">Data de criação</param>
        /// <param name="type">Tipo da mensagem</param>
        /// <param name="content">Conteudo da mensagem (JSON)</param>
        public EventBusMessage(Guid requestId, DateTime createdAt, string type, string content)
        {
            RequestId = requestId;
            CreatedAt = createdAt;
            Type = type;
            Content = content;
        }

        /// <summary>
        /// Id da requisição
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
        /// Conteudo da mensagem (JSON)
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Retorna JSON
        /// </summary>
        /// <returns></returns>
        public override string ToString() => 
            JsonConvert.SerializeObject(this);

        /// <summary>
        /// Retorna array de bytes
        /// </summary>
        /// <returns></returns>
        public ReadOnlyMemory<byte> ToBytes() => 
            Encoding.UTF8.GetBytes(ToString());
    }
}
