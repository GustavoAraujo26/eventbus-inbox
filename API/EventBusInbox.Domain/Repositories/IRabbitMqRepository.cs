using EventBusInbox.Domain.Entities;

namespace EventBusInbox.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório do RabbitMQ
    /// </summary>
    public interface IRabbitMqRepository
    {
        /// <summary>
        /// Inicia o consumo de mensagens das filas
        /// </summary>
        /// <param name="queues">Lista de filas para consumir</param>
        /// <param name="cancellationToken">Token para cancelamento</param>
        /// <returns></returns>
        Task StartConsumption(List<EventBusQueue> queues, CancellationToken cancellationToken);
    }
}
