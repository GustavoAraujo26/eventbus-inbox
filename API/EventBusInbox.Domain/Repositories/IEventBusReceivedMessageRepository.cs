using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Repositories.Base;
using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;

namespace EventBusInbox.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de mensagens recebidas do barramento de eventos
    /// </summary>
    public interface IEventBusReceivedMessageRepository : IBaseRepository<EventBusReceivedMessage>
    {
        /// <summary>
        /// Retorna listagem de mensagens pesquisadas com base em parâmetros de busca recebidos
        /// </summary>
        /// <param name="request">Parâmetros de busca</param>
        /// <returns></returns>
        Task<List<GetEventBusReceivedMessageListResponse>> List(GetEventBusReceivedMessageListRequest request);

        /// <summary>
        /// Retorna listagem de mensagens que estão habilitadas para processamento
        /// </summary>
        /// <param name="request">Parâmetros de busca</param>
        /// <returns></returns>
        Task<List<GetEventBusReceivedMessageToProcessResponse>> List(GetEventBusReceivedMessageToProcessRequest request);

        /// <summary>
        /// Retorna mensagem no padrão de resposta da aplicação, com base no seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetEventBusReceivedMessageResponse> GetResponse(Guid id);

        /// <summary>
        /// Retorna contagem de mensagens por status para cada fila passada como parâmetro
        /// </summary>
        /// <param name="queueIdList">Lista de identificadores de filas</param>
        /// <returns></returns>
        Task<List<KeyValuePair<Guid, SummarizeEventBusReceivedMessagesResponse>>> Summarize(List<Guid> queueIdList);
    }
}
