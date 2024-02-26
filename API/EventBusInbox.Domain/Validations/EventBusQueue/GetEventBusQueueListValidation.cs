using EventBusInbox.Domain.Requests.EventBusQueues;
using FluentValidation;

namespace EventBusInbox.Domain.Validations.EventBusQueue
{
    /// <summary>
    /// Validação para requisição de listagem de filas do barramento de eventos
    /// </summary>
    internal class GetEventBusQueueListValidation : AbstractValidator<GetEventBusQueueListRequest>
    {
        public GetEventBusQueueListValidation()
        {
            RuleFor(x => x.Page).GreaterThan(0).WithMessage("Field must be greater than {0}!");

            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Field must be greater than {0}!");

            RuleFor(x => x.Status).IsInEnum().WithMessage("Invalid field!").When(x => x.Status.HasValue);
        }
    }
}
