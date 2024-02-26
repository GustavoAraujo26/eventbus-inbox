using EventBusInbox.Shared.Models;

namespace EventBusInbox.Domain.Responses.EventBusReceivedMessage
{
    /// <summary>
    /// Resposta relacionada a uma linha do histórico de processamento de uma mensagem recebida do barramento de eventos
    /// </summary>
    public class ProcessingHistoryLineResponse
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public ProcessingHistoryLineResponse() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="occurredAt">Data da ocorrência</param>
        /// <param name="status">Status</param>
        /// <param name="resultMessage">Mensagem de resultado</param>
        public ProcessingHistoryLineResponse(DateTime occurredAt, EnumData status, string resultMessage)
        {
            OccurredAt = occurredAt;
            Status = status;
            ResultMessage = resultMessage;
        }

        /// <summary>
        /// Data da ocorrência
        /// </summary>
        public DateTime OccurredAt { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public EnumData Status { get; set; }

        /// <summary>
        /// Mensagem de resultado
        /// </summary>
        public string ResultMessage { get; set; }
    }
}
