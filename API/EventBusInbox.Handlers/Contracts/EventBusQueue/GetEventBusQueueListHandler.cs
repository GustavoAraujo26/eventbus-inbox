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
    internal class GetEventBusQueueListHandler : IGetEventBusQueueListHandler
    {
        private readonly IEventBusQueueRepository queueRepository;
        private readonly IEventBusReceivedMessageRepository messageRepository;
        private readonly IMediator mediator;

        public GetEventBusQueueListHandler(IEventBusQueueRepository queueRepository, 
            IEventBusReceivedMessageRepository messageRepository, IMediator mediator)
        {
            this.queueRepository = queueRepository;
            this.messageRepository = messageRepository;
            this.mediator = mediator;
        }

        public async Task<AppResponse<GetEventBusQueueResponse>> Handle(GetEventBusQueueListRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return AppResponse<GetEventBusQueueResponse>.Custom(HttpStatusCode.BadRequest, "Invalid request!");

                var validationResponse = request.Validate();
                if (!validationResponse.IsSuccess)
                    return AppResponse<GetEventBusQueueResponse>.Copy(validationResponse);

                var list = await queueRepository.List(request);

                if (request.SummarizeMessages && (list is not null && list.Any()))
                {
                    var summarizationList = await messageRepository.Summarize(list.Select(x => x.Id).ToList());
                    EventBusQueueFactory.LinkMessageSummarization(list, summarizationList);
                }

                return AppResponse<GetEventBusQueueResponse>.Success(list);
            }
            catch(Exception ex)
            {
                await mediator.Publish(EventLogNotification.Create(this, ex,
                    $"An error occurred when retrieving event bus queue list!"));
                return AppResponse<GetEventBusQueueResponse>.Error(ex);
            }
        }
    }
}
