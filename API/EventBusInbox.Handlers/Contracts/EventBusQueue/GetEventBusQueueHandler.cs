using EventBusInbox.Domain.Factories;
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
        private readonly IEventBusQueueRepository queueRepository;
        private readonly IEventBusReceivedMessageRepository messageRepository;
        private readonly IMediator mediator;

        public GetEventBusQueueHandler(IEventBusQueueRepository queueRepository, 
            IEventBusReceivedMessageRepository messageRepository, IMediator mediator)
        {
            this.queueRepository = queueRepository;
            this.messageRepository = messageRepository;
            this.mediator = mediator;
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

                var queue = await queueRepository.Get(request);

                if (request.SummarizeMessages && queue is not null)
                {
                    var summarizationList = await messageRepository.Summarize(new List<Guid>{ queue.Id });
                    EventBusQueueFactory.LinkMessageSummarization(queue, summarizationList);
                }

                return AppResponse<GetEventBusQueueResponse>.Success(queue);
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
