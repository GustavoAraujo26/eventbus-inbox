using EventBusInbox.Domain.Requests.EventBusQueues;
using FluentValidation;

namespace EventBusInbox.Domain.Validations.EventBusQueue
{
    /// <summary>
    /// Validação da requisição de persistência de fila do barramento de eventos
    /// </summary>
    internal class SaveEventBusQueueValidation : AbstractValidator<SaveEventBusQueueRequest>
    {
        public SaveEventBusQueueValidation()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Invalid Field!");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Field is required!")
                .NotEmpty().WithMessage("Field is required!")
                .MaximumLength(100).WithMessage("Field must have {0} chars or less!");

            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("Field must have {0} chars or less!")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Status).IsInEnum().WithMessage("Field invalid!");

            RuleFor(x => x.ProcessingAttempts).GreaterThan(0).WithMessage("Field must be greater than {0}");
        }
    }
}
