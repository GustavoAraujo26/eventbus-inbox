using AutoMapper;
using EventBusInbox.Domain.Handlers.EventBusQueue;
using EventBusInbox.Domain.Notifications;
using EventBusInbox.Domain.Repositories;
using EventBusInbox.Domain.Requests.EventBusQueues;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Shared.Models;
using MediatR;
using System.Net;

namespace EventBusInbox.Handlers.Contracts.EventBusQueue
{
    public class GetEventBusQueueHandler : IGetEventBusQueueHandler
    {
        private readonly IEventBusQueueRepository repository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public GetEventBusQueueHandler(IEventBusQueueRepository repository, IMediator mediator, IMapper mapper)
        {
            this.repository = repository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<AppResponse<GetEventBusQueueResponse>> Handle(GetEventBusQueueRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return AppResponse<GetEventBusQueueResponse>.Custom(HttpStatusCode.BadRequest, "Invalid request!");

                var validationResponse = request.Validate();
                if (!validationResponse.IsSuccess)
                    return AppResponse<GetEventBusQueueResponse>.Copy(validationResponse);

                Domain.Entities.EventBusQueue queue;

                if (request.Id.HasValue)
                    queue = await repository.GetById(request.Id.Value);
                else
                    queue = await repository.GetByName(request.Name);

                return AppResponse<GetEventBusQueueResponse>.Success(mapper.Map<GetEventBusQueueResponse>(queue));
            }
            catch(Exception ex)
            {
                await mediator.Publish(EventLogNotification.Create(this, ex,
                    $"An error occurred when retrieving event bus queue {request.Id}!"));
                return AppResponse<GetEventBusQueueResponse>.Error(ex);
            }
        }
    }
}
