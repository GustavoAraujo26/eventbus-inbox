using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Handlers.EventBusReceivedMessage
{
    /// <summary>
    /// Interface do manipulador de reativação de mensagem recebida no barramento de eventos
    /// </summary>
    public interface IReactivateEventBusReceivedMessageHandler : 
        IRequestHandler<ReactivateEventBusReceivedMessageRequest, AppResponse<AppTaskResponse>>
    {
    }
}
