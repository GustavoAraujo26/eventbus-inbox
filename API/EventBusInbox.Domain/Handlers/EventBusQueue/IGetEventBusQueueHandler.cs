using EventBusInbox.Domain.Requests.EventBusQueues;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Handlers.EventBusQueue
{
    /// <summary>
    /// Interface do manipulador de pesquisa de fila do barramento de eventos
    /// </summary>
    public interface IGetEventBusQueueHandler : IRequestHandler<GetEventBusQueueRequest, AppResponse<GetEventBusQueueResponse>>
    {
    }
}
