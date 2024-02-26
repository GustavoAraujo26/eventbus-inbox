using EventBusInbox.Domain.Requests.EventBusQueues;
using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Handlers.EventBusQueue
{
    /// <summary>
    /// Interface do manipulador de persistência de fila do barramento de eventos
    /// </summary>
    public interface ISaveEventBusQueueHandler : IRequestHandler<SaveEventBusQueueRequest, AppResponse<AppTaskResponse>>
    {
    }
}
