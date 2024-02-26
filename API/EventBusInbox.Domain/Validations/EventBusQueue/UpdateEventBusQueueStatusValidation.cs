using EventBusInbox.Domain.Requests.EventBusQueues;
using FluentValidation;

namespace EventBusInbox.Domain.Validations.EventBusQueue
{
    /// <summary>
    /// Validação da requisição de atualização do status da fila do barramento de eventos
    /// </summary>
    internal class UpdateEventBusQueueStatusValidation : AbstractValidator<UpdateEventBusQueueStatusRequest>
    {
        public UpdateEventBusQueueStatusValidation()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("{PropertyName} invalid!");

            RuleFor(x => x.Status).IsInEnum().WithMessage("Field invalid!");
        }
    }
}
