using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Handlers.EventBusQueue;
using EventBusInbox.Domain.Notifications;
using EventBusInbox.Domain.Repositories;
using EventBusInbox.Domain.Requests.EventBusQueues;
using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using MediatR;
using System.Net;

namespace EventBusInbox.Handlers.Contracts.EventBusQueue
{
    public class UpdateEventBusQueueStatusHandler : IUpdateEventBusQueueStatusHandler
    {
        private readonly IEventBusQueueRepository repository;
        private readonly IMediator mediator;

        public UpdateEventBusQueueStatusHandler(IEventBusQueueRepository repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        public async Task<AppResponse<AppTaskResponse>> Handle(UpdateEventBusQueueStatusRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return AppResponse<AppTaskResponse>.Custom(HttpStatusCode.BadRequest, "Invalid request!");

                var validationResponse = request.Validate();
                if (!validationResponse.IsSuccess)
                    return AppResponse<AppTaskResponse>.Copy(validationResponse);

                var currentQueue = await repository.GetById(request.Id);
                if (currentQueue is null)
                    return AppResponse<AppTaskResponse>.Custom(HttpStatusCode.NotFound, "Queue not found!");

                if (request.Status is QueueStatus.Enabled)
                    currentQueue.Enable();
                else
                    currentQueue.Disable();

                var repositoryResponse = await repository.Save(currentQueue);
                if (!repositoryResponse.IsSuccess)
                    return AppResponse<AppTaskResponse>.Copy(repositoryResponse);

                await mediator.Publish(EventLogNotification.Create(this, $"Event bus queue status {request.Id} updated!"));

                var responseContent = new List<AppTaskResponse> { new AppTaskResponse(request.Id) };
                return AppResponse<AppTaskResponse>.Success(responseContent);
            }
            catch (Exception ex)
            {
                await mediator.Publish(EventLogNotification.Create(this, ex,
                    $"An error occurred when updating event bus queue status {request.Id}!"));
                return AppResponse<AppTaskResponse>.Error(ex);
            }
        }
    }
}
