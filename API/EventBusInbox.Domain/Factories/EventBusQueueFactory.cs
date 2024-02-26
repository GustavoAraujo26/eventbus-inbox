using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;

namespace EventBusInbox.Domain.Factories
{
    /// <summary>
    /// Fábrica para manipulação das filas do barramento de eventos
    /// </summary>
    public static class EventBusQueueFactory
    {
        /// <summary>
        /// Vincula as sumarizações de mensagens às suas respectivas filas
        /// </summary>
        /// <param name="queueList">Lista de filas</param>
        /// <param name="messageSummarizationList">Lista de sumarizações de mensagens</param>
        public static void LinkMessageSummarization(List<GetEventBusQueueResponse> queueList, 
            List<KeyValuePair<Guid, SummarizeEventBusReceivedMessagesResponse>> messageSummarizationList)
        {
            if (queueList is null || !queueList.Any()) 
                return;

            if (messageSummarizationList is null || !messageSummarizationList.Any()) 
                return;

            foreach(var queue in queueList)
            {
                var summarizationList = messageSummarizationList.Where(x => x.Key.Equals(queue.Id)).ToList();
                if (summarizationList is not null && summarizationList.Any())
                    LinkMessageSummarization(queue, summarizationList);
            }
        }

        /// <summary>
        /// Vincula as sumarizações à uma fila
        /// </summary>
        /// <param name="queue">Fila do barramento de eventos</param>
        /// <param name="messageSummarizationList">Lista de sumarizações de mensagens</param>
        public static void LinkMessageSummarization(GetEventBusQueueResponse queue,
            List<KeyValuePair<Guid, SummarizeEventBusReceivedMessagesResponse>> messageSummarizationList) =>
            queue.MessagesSummarization = messageSummarizationList.Select(x => x.Value).ToList();
    }
}
