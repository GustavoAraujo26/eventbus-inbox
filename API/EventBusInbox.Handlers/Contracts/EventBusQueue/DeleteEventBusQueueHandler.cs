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
    public class DeleteEventBusQueueHandler : IDeleteEventBusQueueHandler
    {
        private readonly IEventBusQueueRepository repository;
        private readonly IMediator mediator;

        public DeleteEventBusQueueHandler(IEventBusQueueRepository repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        public async Task<AppResponse<EventBusQueueResponse>> Handle(DeleteEventBusQueueRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return AppResponse<EventBusQueueResponse>.Custom(HttpStatusCode.BadRequest, "Invalid request!");

                var validationResponse = request.Validate();
                if (!validationResponse.IsSuccess)
                    return AppResponse<EventBusQueueResponse>.Copy(validationResponse);

                var currentQueue = await repository.GetById(request.Id);
                if (currentQueue is null)
                    return AppResponse<EventBusQueueResponse>.Custom(HttpStatusCode.NotFound, "Queue not found!");

                var repositoryResponse = await repository.Delete(request.Id);
                if (!repositoryResponse.IsSuccess)
                    return AppResponse<EventBusQueueResponse>.Copy(repositoryResponse);

                await mediator.Publish(EventLogNotification.Create(this, $"Event bus queue {request.Id} deleted!"));

                var responseContent = new List<EventBusQueueResponse> { new EventBusQueueResponse(request.Id) };
                return AppResponse<EventBusQueueResponse>.Success(responseContent);
            }
            catch(Exception ex)
            {
                await mediator.Publish(EventLogNotification.Create(this, ex, 
                    $"An error occurred when deleting event bus queue {request.Id}!"));
                return AppResponse<EventBusQueueResponse>.Error(ex);
            }
        }
    }
}
