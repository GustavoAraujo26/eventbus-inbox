using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Handlers.EventBusReceivedMessage
{
    /// <summary>
    /// Interface do manipulador de persistência de mensagem recebida do barramento de eventos
    /// </summary>
    public interface ISaveEventBusReceivedMessageHandler : 
        IRequestHandler<SaveEventBusReceivedMessageRequest, AppResponse<AppTaskResponse>>
    {
    }
}
