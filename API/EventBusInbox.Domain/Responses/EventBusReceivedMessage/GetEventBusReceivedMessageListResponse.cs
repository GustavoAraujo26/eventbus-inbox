using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Shared.Models;

namespace EventBusInbox.Domain.Responses.EventBusReceivedMessage
{
    /// <summary>
    /// Resposta de retorno de lista de mensagens recebidas no barramento de eventos
    /// </summary>
    public class GetEventBusReceivedMessageListResponse
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public GetEventBusReceivedMessageListResponse() { }

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
        /// Fila na qual a mensagem foi recebida
        /// </summary>
        public GetEventBusQueueResponse Queue { get; set; }

        /// <summary>
        /// Status da mensagem
        /// </summary>
        public EnumData Status { get; set; }

        /// <summary>
        /// Quantidade de processamentos
        /// </summary>
        public int ProcessingAttempts { get; set; }

        /// <summary>
        /// Última alteração no histórico de processamento
        /// </summary>
        public ProcessingHistoryLineResponse LastUpdate { get; set; }
    }
}
