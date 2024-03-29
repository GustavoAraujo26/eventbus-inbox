﻿using EventBusInbox.Domain.Enums;
using EventBusInbox.Shared.Extensions;
using Newtonsoft.Json;
using System.Net;

namespace EventBusInbox.Domain.Entities
{
    /// <summary>
    /// Entidade que representa mensagem recebida no barramento de eventos
    /// </summary>
    public class EventBusReceivedMessage
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public EventBusReceivedMessage() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="requestId">Id da requisição</param>
        /// <param name="createdAt">Data de criação</param>
        /// <param name="type">Tipo da mensagem</param>
        /// <param name="content">Conteudo da mensagem (JSON)</param>
        /// <param name="queue">Fila na qual a mensagem foi recebida</param>
        /// <param name="status">Status da mensagem</param>
        /// <param name="processingAttempts">Quantidade de processamentos</param>
        /// <param name="processingHistory">Histórico de processamento da mensagem</param>
        public EventBusReceivedMessage(Guid requestId, DateTime createdAt, string type, 
            string content, EventBusQueue queue, EventBusMessageStatus status, int processingAttempts, 
            IList<ProcessingHistoryLine> processingHistory)
        {
            RequestId = requestId;
            CreatedAt = createdAt;
            Type = type;
            Content = content;
            Queue = queue;
            Status = status;
            ProcessingAttempts = processingAttempts;
            ProcessingHistory = processingHistory;
        }

        /// <summary>
        /// Id da requisição
        /// </summary>
        public Guid RequestId { get; private set; }

        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Tipo da mensagem
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Conteudo da mensagem (JSON)
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Fila na qual a mensagem foi recebida
        /// </summary>
        public EventBusQueue Queue { get; private set; }

        /// <summary>
        /// Status da mensagem
        /// </summary>
        public EventBusMessageStatus Status { get; private set; }

        /// <summary>
        /// Quantidade de processamentos
        /// </summary>
        public int ProcessingAttempts { get; private set; }

        /// <summary>
        /// Histórico de processamento da mensagem
        /// </summary>
        public IList<ProcessingHistoryLine> ProcessingHistory { get; private set; }

        /// <summary>
        /// Última atualização do histórico
        /// </summary>
        public ProcessingHistoryLine LastUpdate
        {
            get
            {
                if (ProcessingHistory is null || !ProcessingHistory.Any())
                    return null;

                return ProcessingHistory.OrderByDescending(x => x.OccurredAt).FirstOrDefault();
            }
        }

        /// <summary>
        /// Retorna JSON
        /// </summary>
        /// <returns></returns>
        public override string ToString() => 
            JsonConvert.SerializeObject(this);

        /// <summary>
        /// Realiza cópia dos dados
        /// </summary>
        /// <returns></returns>
        public EventBusReceivedMessage Copy() =>
            JsonConvert.DeserializeObject<EventBusReceivedMessage>(this.ToString());

        /// <summary>
        /// Cria nova entidade
        /// </summary>
        /// <param name="requestId">Identificador da requisição</param>
        /// <param name="createdAt">Data de criação</param>
        /// <param name="type">Tipo da mensagem</param>
        /// <param name="content">Conteudo da mensagem (JSON)</param>
        /// <returns></returns>
        public static EventBusReceivedMessage Create(Guid requestId, DateTime createdAt, string type, string content) =>
            new EventBusReceivedMessage
            {
                RequestId = requestId,
                CreatedAt = createdAt,
                Type = type,
                Content = content,
                ProcessingHistory = new List<ProcessingHistoryLine>
                {
                    ProcessingHistoryLine.Create(createdAt, HttpStatusCode.Accepted, "Message received successfully!")
                },
                ProcessingAttempts = 0,
                Status = EventBusMessageStatus.Pending
            };

        /// <summary>
        /// Verifica se a quantidade de tentativas de processamento já foi alcançada
        /// </summary>
        /// <returns></returns>
        public bool AttemptLimitReached() => ProcessingAttempts <= Queue.ProcessingAttempts;

        /// <summary>
        /// Reabilita mensagem para ser processada novamente
        /// </summary>
        public void Reactivate()
        {
            Status = EventBusMessageStatus.Pending;
            ProcessingAttempts = 0;
            ProcessingHistory.Add(
                ProcessingHistoryLine.Create(DateTime.Now, HttpStatusCode.Accepted, "Message changed to reprocess!")
            );
        }

        /// <summary>
        /// Atribui resultado para a mensagem em questão
        /// </summary>
        /// <param name="statusCode">Código do status HTTP</param>
        /// <param name="message">Mensagem do resultado</param>
        public void SetResult(HttpStatusCode statusCode, string message)
        {
            ProcessingAttempts++;
            ProcessingHistory.Add(
                ProcessingHistoryLine.Create(DateTime.Now, statusCode, message)
            );

            Status = statusCode.ToMessageStatus();

            if (!statusCode.IsSuccess() && ProcessingAttempts >= Queue.ProcessingAttempts)
            {
                Status = EventBusMessageStatus.PermanentFailure;
                ProcessingHistory.Add(
                    ProcessingHistoryLine.Create(DateTime.Now, HttpStatusCode.Locked, "Processing attempt limit reached!")
                );
            }
        }

        /// <summary>
        /// Atualiza dados básicos da mensagem
        /// </summary>
        /// <param name="createdAt">Data de criação</param>
        /// <param name="type">Tipo da mensagem</param>
        /// <param name="data">Conteudo da mensagem</param>
        public void UpdateBasicData(DateTime createdAt, string type, dynamic data)
        {
            CreatedAt = createdAt;
            Type = type;
            Content = data;
        }

        /// <summary>
        /// Adiciona vínculo com uma fila
        /// </summary>
        /// <param name="queue"></param>
        public void AddQueue(EventBusQueue queue) =>
            Queue = queue;
    }
}
