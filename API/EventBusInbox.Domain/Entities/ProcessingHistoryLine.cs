using Newtonsoft.Json;
using System.Net;

namespace EventBusInbox.Domain.Entities
{
    /// <summary>
    /// Entidade que representa uma linha do histório de processamento 
    /// de uma mensagem recebida do barramento de eventos
    /// </summary>
    public class ProcessingHistoryLine
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public ProcessingHistoryLine() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="occurredAt">Data de ocorrência</param>
        /// <param name="resultStatus">Status HTTP do resultado do processamento</param>
        /// <param name="resultMessage">Mensagem do resultado do processamento</param>
        private ProcessingHistoryLine(DateTime occurredAt, HttpStatusCode resultStatus, string resultMessage)
        {
            OccurredAt = occurredAt;
            ResultStatus = resultStatus;
            ResultMessage = resultMessage;
        }

        /// <summary>
        /// Data de ocorrência
        /// </summary>
        public DateTime OccurredAt { get; private set; }

        /// <summary>
        /// Status HTTP do resultado do processamento
        /// </summary>
        public HttpStatusCode ResultStatus { get; private set; }

        /// <summary>
        /// Mensagem do resultado do processamento
        /// </summary>
        public string ResultMessage { get; private set; }

        /// <summary>
        /// Retorna JSON
        /// </summary>
        /// <returns></returns>
        public override string ToString() => 
            JsonConvert.SerializeObject(this);

        /// <summary>
        /// Realiza cópia dos dados
        /// </summary>
        /// <param name="origin">Origem da cópia</param>
        /// <returns></returns>
        public ProcessingHistoryLine Copy() =>
            JsonConvert.DeserializeObject<ProcessingHistoryLine>(this.ToString());

        /// <summary>
        /// Realiza a criação do objeto
        /// </summary>
        /// <param name="occurredAt">Data de ocorrência</param>
        /// <param name="resultStatus">Status HTTP do resultado do processamento</param>
        /// <param name="resultMessage">Mensagem do resultado do processamento</param>
        /// <returns></returns>
        public static ProcessingHistoryLine Create(DateTime occurredAt, HttpStatusCode resultStatus, string resultMessage) =>
            new ProcessingHistoryLine(occurredAt, resultStatus, resultMessage);
    }
}
