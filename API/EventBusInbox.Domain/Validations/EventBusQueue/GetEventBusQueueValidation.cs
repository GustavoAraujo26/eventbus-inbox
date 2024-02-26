using EventBusInbox.Domain.Requests.EventBusQueues;
using FluentValidation;

namespace EventBusInbox.Domain.Validations.EventBusQueue
{
    /// <summary>
    /// Validação para a requisição de busca de fila do barramento de eventos
    /// </summary>
    internal class GetEventBusQueueValidation : AbstractValidator<GetEventBusQueueRequest>
    {
        public GetEventBusQueueValidation()
        {
            RuleFor(x => x).Custom((obj, context) =>
            {
                if (obj.Id.HasValue && !string.IsNullOrEmpty(obj.Name))
                    context.AddFailure("Only fill in Id OR name!");
                else if (!obj.Id.HasValue && string.IsNullOrEmpty(obj.Name))
                    context.AddFailure("Id or name must be filled in!");
            });

            When(x => x.Id.HasValue, () =>
            {
                RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Invalid field!");
            });

            When(x => !string.IsNullOrEmpty(x.Name), () =>
            {
                RuleFor(x => x.Name).MaximumLength(100).WithMessage("Field must have {0} chars or less!");
            });
        }
    }
}
