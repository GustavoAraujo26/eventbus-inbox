using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using FluentValidation;

namespace EventBusInbox.Domain.Validations.EventBusReceivedMessage
{
    /// <summary>
    /// Validação para requisição para atualização do status da mensagem recebida do barramento de eventos
    /// </summary>
    internal class UpdateEventBusReceivedMessageStatusValidation : AbstractValidator<UpdateEventBusReceivedMessageStatusRequest>
    {
        public UpdateEventBusReceivedMessageStatusValidation()
        {
            RuleFor(x => x.RequestId).NotEqual(Guid.Empty).WithMessage("Invalid field!");

            RuleFor(x => x.ProcessStatus).IsInEnum().WithMessage("Invalid field!");

            RuleFor(x => x.ResultMessage)
                .NotNull().WithMessage("Invalid field!")
                .NotEmpty().WithMessage("Invalid field!");
        }
    }
}
