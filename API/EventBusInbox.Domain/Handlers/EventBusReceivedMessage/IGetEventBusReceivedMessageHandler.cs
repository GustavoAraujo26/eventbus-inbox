using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Handlers.EventBusReceivedMessage
{
    /// <summary>
    /// Interface do manipulador de busca de mensagem recebida no barramento de eventos
    /// </summary>
    public interface IGetEventBusReceivedMessageHandler : 
        IRequestHandler<GetEventBusReceivedMessageRequest, AppResponse<GetEventBusReceivedMessageResponse>>
    {
    }
}
