using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Handlers.EventBusReceivedMessage
{
    /// <summary>
    /// Interface do manipulador de listagem de mensagens recebidas do barramento de eventos para processamento
    /// </summary>
    public interface IGetEventBusReceivedMessageToProcessHandler : 
        IRequestHandler<GetEventBusReceivedMessageToProcessRequest, AppResponse<GetEventBusReceivedMessageToProcessResponse>>
    {
    }
}
