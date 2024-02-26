using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using FluentValidation;

namespace EventBusInbox.Domain.Validations.EventBusReceivedMessage
{
    /// <summary>
    /// Validação de requisição de listagem de mensagens recebidas do barramento de eventos
    /// </summary>
    internal class GetEventBusReceivedMessageListValidation : AbstractValidator<GetEventBusReceivedMessageListRequest>
    {
        public GetEventBusReceivedMessageListValidation()
        {
            RuleFor(x => x.QueueId).NotEqual(Guid.Empty).WithMessage("Invalid field!").When(x => x.QueueId.HasValue);

            When(x => x.CreationDateSearch is not null, () =>
            {
                RuleFor(x => x.CreationDateSearch.Start)
                    .NotEqual(DateTime.MinValue).WithMessage("Invalid field!")
                    .When(x => x.CreationDateSearch.Start.HasValue);

                RuleFor(x => x.CreationDateSearch.End)
                    .NotEqual(DateTime.MinValue).WithMessage("Invalid field!")
                    .When(x => x.CreationDateSearch.End.HasValue);

                RuleFor(x => x.CreationDateSearch.Start)
                    .LessThanOrEqualTo(x => x.CreationDateSearch.End)
                    .WithMessage("Field must be less or equal to {0}")
                    .When(x => x.CreationDateSearch.Start.HasValue && x.CreationDateSearch.End.HasValue);
            });

            When(x => x.UpdateDateSearch is not null, () =>
            {
                RuleFor(x => x.UpdateDateSearch.Start)
                    .NotEqual(DateTime.MinValue).WithMessage("Invalid field!")
                    .When(x => x.UpdateDateSearch.Start.HasValue);

                RuleFor(x => x.UpdateDateSearch.End)
                    .NotEqual(DateTime.MinValue).WithMessage("Invalid field!")
                    .When(x => x.UpdateDateSearch.End.HasValue);

                RuleFor(x => x.UpdateDateSearch.Start)
                    .LessThanOrEqualTo(x => x.UpdateDateSearch.End)
                    .WithMessage("Field must be less or equal to {0}")
                    .When(x => x.UpdateDateSearch.Start.HasValue && x.UpdateDateSearch.End.HasValue);
            });

            RuleForEach(x => x.StatusToSearch)
                .IsInEnum().WithMessage("Invalid field!")
                .When(x => x.StatusToSearch is not null && x.StatusToSearch.Any());

            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Field must be greater than {0}");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Field must be greater than {0}");

            RuleFor(x => x).Custom((obj, context) =>
            {
                bool creationDateFilled = obj.CreationDateSearch is not null && 
                    obj.CreationDateSearch.Start.HasValue && obj.CreationDateSearch.End.HasValue;

                bool updateDateFilled = obj.UpdateDateSearch is not null &&
                    obj.UpdateDateSearch.Start.HasValue && obj.UpdateDateSearch.End.HasValue;

                if (creationDateFilled && updateDateFilled)
                    context.AddFailure("Only fill in creation search period OR update search period!");
            });
        }
    }
}
