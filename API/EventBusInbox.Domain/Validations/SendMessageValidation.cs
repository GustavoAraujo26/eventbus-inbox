using EventBusInbox.Domain.Requests;
using FluentValidation;

namespace EventBusInbox.Domain.Validations
{
    /// <summary>
    /// Validação para requisição para envio de mensagem no RabbitMQ
    /// </summary>
    internal class SendMessageValidation : AbstractValidator<SendMessageRequest>
    {
        public SendMessageValidation()
        {
            RuleFor(x => x.RequestId).NotEqual(Guid.Empty).WithMessage("Invalid field!");

            RuleFor(x => x.CreatedAt).NotEqual(DateTime.MinValue).WithMessage("Invalid field!");

            RuleFor(x => x.Type)
                .NotNull().WithMessage("Field is required!")
                .NotEmpty().WithMessage("Field is required!");

            RuleFor(x => x.Content).NotNull().WithMessage("Invalid field!");

            RuleFor(x => x.QueueId).NotEqual(Guid.Empty).WithMessage("Invalid field!");
        }
    }
}
