using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Shared.Models;

namespace EventBusInbox.Domain.Responses.EventBusReceivedMessage
{
    /// <summary>
    /// Resposta de retorno de uma mensagem recebida no barramento de eventos
    /// </summary>
    public class GetEventBusReceivedMessageResponse
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public GetEventBusReceivedMessageResponse() { }

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

        /// <summary>
        /// Status da mensagem
        /// </summary>
        public EnumData Status { get; set; }

        /// <summary>
        /// Quantidade de processamentos
        /// </summary>
        public int ProcessingAttempts { get; set; }

        /// <summary>
        /// Histórico de processamento da mensagem
        /// </summary>
        public IList<ProcessingHistoryLineResponse> ProcessingHistory { get; set; }
    }
}
