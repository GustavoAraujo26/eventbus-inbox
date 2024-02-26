using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using FluentValidation;

namespace EventBusInbox.Domain.Validations.EventBusReceivedMessage
{
    /// <summary>
    /// Validação para requisição de deleção de mensagem recebida do barramento de eventos
    /// </summary>
    internal class DeleteEventBusReceivedMessageValidation : AbstractValidator<DeleteEventBusReceivedMessageRequest>
    {
        public DeleteEventBusReceivedMessageValidation()
        {
            RuleFor(x => x.RequestId).NotEqual(Guid.Empty).WithMessage("Invalid field!");
        }
    }
}
