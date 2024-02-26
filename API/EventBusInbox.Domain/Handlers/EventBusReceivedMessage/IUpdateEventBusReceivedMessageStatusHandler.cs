using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Handlers.EventBusReceivedMessage
{
    /// <summary>
    /// Interface do manipulador de atualização do status da mensagem recebida do barramento de eventos
    /// </summary>
    public interface IUpdateEventBusReceivedMessageStatusHandler : 
        IRequestHandler<UpdateEventBusReceivedMessageStatusRequest, AppResponse<AppTaskResponse>>
    {
    }
}
