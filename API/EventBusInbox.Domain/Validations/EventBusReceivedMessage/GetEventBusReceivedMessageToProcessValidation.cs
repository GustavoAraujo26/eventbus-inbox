using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using FluentValidation;

namespace EventBusInbox.Domain.Validations.EventBusReceivedMessage
{
    /// <summary>
    /// Validação para requisição para listagem de mensagens recebidas do barramento de eventos para processamento
    /// </summary>
    internal class GetEventBusReceivedMessageToProcessValidation : AbstractValidator<GetEventBusReceivedMessageToProcessRequest>
    {
        public GetEventBusReceivedMessageToProcessValidation()
        {
            RuleFor(x => x.QueueId).NotEqual(Guid.Empty).WithMessage("Invalid field!");
        }
    }
}
