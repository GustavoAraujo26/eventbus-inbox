using EventBusInbox.Shared.Models;

namespace EventBusInbox.Domain.Responses.EventBusReceivedMessage
{
    /// <summary>
    /// Resposta de sumarização de mensagens recebidas do barramento de eventos
    /// </summary>
    public class SummarizeEventBusReceivedMessagesResponse
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public SummarizeEventBusReceivedMessagesResponse() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="status">Status das mensagens</param>
        /// <param name="messageCount">Quantidade de mensagens</param>
        public SummarizeEventBusReceivedMessagesResponse(EnumData status, long messageCount)
        {
            Status = status;
            MessageCount = messageCount;
        }

        /// <summary>
        /// Status das mensagens
        /// </summary>
        public EnumData Status { get; set; }

        /// <summary>
        /// Quantidade de mensagens
        /// </summary>
        public long MessageCount { get; set; }
    }
}
