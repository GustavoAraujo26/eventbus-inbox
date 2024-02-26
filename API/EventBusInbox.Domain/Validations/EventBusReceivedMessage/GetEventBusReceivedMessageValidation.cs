using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using FluentValidation;

namespace EventBusInbox.Domain.Validations.EventBusReceivedMessage
{
    /// <summary>
    /// Validação para requisição de busca de mensagem recebida no barramento de eventos
    /// </summary>
    internal class GetEventBusReceivedMessageValidation : AbstractValidator<GetEventBusReceivedMessageRequest>
    {
        public GetEventBusReceivedMessageValidation()
        {
            RuleFor(x => x.RequestId).NotEqual(Guid.Empty).WithMessage("Invalid field!");
        }
    }
}
