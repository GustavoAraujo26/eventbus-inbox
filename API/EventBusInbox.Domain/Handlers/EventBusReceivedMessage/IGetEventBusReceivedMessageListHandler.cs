using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Handlers.EventBusReceivedMessage
{
    /// <summary>
    /// Interface do manipulador de listagem de mensagens recebidas do barramento de eventos
    /// </summary>
    public interface IGetEventBusReceivedMessageListHandler : 
        IRequestHandler<GetEventBusReceivedMessageListRequest, AppResponse<GetEventBusReceivedMessageListResponse>>
    {
    }
}
