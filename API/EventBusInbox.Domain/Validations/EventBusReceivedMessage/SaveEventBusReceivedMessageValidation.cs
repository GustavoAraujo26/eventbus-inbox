using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using FluentValidation;

namespace EventBusInbox.Domain.Validations.EventBusReceivedMessage
{
    /// <summary>
    /// Validação de requisição para persistência de mensagem recebida do barramento de eventos
    /// </summary>
    internal class SaveEventBusReceivedMessageValidation : AbstractValidator<SaveEventBusReceivedMessageRequest>
    {
        public SaveEventBusReceivedMessageValidation()
        {
            RuleFor(x => x.RequestId).NotEqual(Guid.Empty).WithMessage("Invalid field!");

            RuleFor(x => x.CreatedAt).NotEqual(DateTime.MinValue).WithMessage("Invalid field!");

            RuleFor(x => x.Type)
                .NotNull().WithMessage("Field is required!")
                .NotEmpty().WithMessage("Field is required!");

            RuleFor(x => x.Data).NotNull().WithMessage("Invalid field!");

            RuleFor(x => x.QueueId).NotEqual(Guid.Empty).WithMessage("Invalid field!");
        }
    }
}
