using EventBusInbox.Domain.Requests.EventBusQueues;
using FluentValidation;

namespace EventBusInbox.Domain.Validations.EventBusQueue
{
    /// <summary>
    /// Validação para a requisição de deleção de fila do barramento de eventos
    /// </summary>
    internal class DeleteEventBusQueueValidation : AbstractValidator<DeleteEventBusQueueRequest>
    {
        public DeleteEventBusQueueValidation()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Invalid field!");
        }
    }
}
