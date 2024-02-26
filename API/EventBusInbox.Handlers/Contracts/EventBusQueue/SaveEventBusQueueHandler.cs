using AutoMapper;
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
    public class SaveEventBusQueueHandler : ISaveEventBusQueueHandler
    {
        private readonly IEventBusQueueRepository repository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SaveEventBusQueueHandler(IEventBusQueueRepository repository, IMediator mediator, IMapper mapper)
        {
            this.repository = repository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<AppResponse<AppTaskResponse>> Handle(SaveEventBusQueueRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return AppResponse<AppTaskResponse>.Custom(HttpStatusCode.BadRequest, "Invalid request!");

                var validationResponse = request.Validate();
                if (!validationResponse.IsSuccess)
                    return AppResponse<AppTaskResponse>.Copy(validationResponse);

                var currentQueue = mapper.Map<Domain.Entities.EventBusQueue>(request);

                var existingQueue = await repository.GetByName(request.Name);
                if (existingQueue is not null && !currentQueue.Id.Equals(existingQueue.Id))
                    return AppResponse<AppTaskResponse>.Custom(HttpStatusCode.BadRequest, 
                        $"Already exists queue with same name (Id: {existingQueue.Id})");

                var repositoryResponse = await repository.Save(currentQueue);
                if (!repositoryResponse.IsSuccess)
                    return AppResponse<AppTaskResponse>.Copy(repositoryResponse);

                await mediator.Publish(EventLogNotification.Create(this, $"Event bus queue {request.Id} saved!"));

                var responseContent = new List<AppTaskResponse> { new AppTaskResponse(request.Id) };
                return AppResponse<AppTaskResponse>.Success(responseContent);
            }
            catch(Exception ex)
            {
                await mediator.Publish(EventLogNotification.Create(this, ex,
                    $"An error occurred when saving event bus queue {request.Id}!"));
                return AppResponse<AppTaskResponse>.Error(ex);
            }
        }
    }
}
