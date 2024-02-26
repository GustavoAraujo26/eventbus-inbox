using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using FluentValidation;

namespace EventBusInbox.Domain.Validations.EventBusReceivedMessage
{
    /// <summary>
    /// Validação para Requisição de reativação de mensagem recebida no barramento de eventos
    /// </summary>
    internal class ReactivateEventBusReceivedMessageValidation : AbstractValidator<ReactivateEventBusReceivedMessageRequest>
    {
        public ReactivateEventBusReceivedMessageValidation()
        {
            RuleFor(x => x.RequestId).NotEqual(Guid.Empty).WithMessage("Invalid field!");
        }
    }
}
