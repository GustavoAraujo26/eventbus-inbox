using EventBusInbox.Domain.Requests;
using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using MediatR;

namespace EventBusInbox.Domain.Handlers
{
    /// <summary>
    /// Interface do manipulador de envio de mensagem para o RabbitMQ
    /// </summary>
    public interface ISendMessageHandler : IRequestHandler<SendMessageRequest, AppResponse<AppTaskResponse>>
    {
    }
}
