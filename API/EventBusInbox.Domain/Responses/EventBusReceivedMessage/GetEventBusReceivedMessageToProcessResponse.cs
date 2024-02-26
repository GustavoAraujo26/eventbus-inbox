using EventBusInbox.Domain.Responses.EventBusQueues;

namespace EventBusInbox.Domain.Responses.EventBusReceivedMessage
{
    /// <summary>
    /// Resposta para busca de mensagem recebida do barramento de eventos para processamento
    /// </summary>
    public class GetEventBusReceivedMessageToProcessResponse
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public GetEventBusReceivedMessageToProcessResponse() { }

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
        /// Conteudo da mensagem
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// Fila na qual a mensagem foi recebida
        /// </summary>
        public GetEventBusQueueResponse Queue { get; set; }
    }
}
