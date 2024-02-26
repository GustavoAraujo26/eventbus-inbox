using AutoMapper;
using EventBusInbox.Domain.Handlers.EventBusReceivedMessage;
using EventBusInbox.Domain.Notifications;
using EventBusInbox.Domain.Repositories;
using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using MediatR;
using System.Net;

namespace EventBusInbox.Handlers.Contracts.EventBusReceivedMessage
{
    internal class SaveEventBusReceivedMessageHandler : ISaveEventBusReceivedMessageHandler
    {
        private readonly IEventBusReceivedMessageRepository messageRepository;
        private readonly IEventBusQueueRepository queueRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SaveEventBusReceivedMessageHandler(IEventBusReceivedMessageRepository messageRepository, 
            IEventBusQueueRepository queueRepository, IMediator mediator, IMapper mapper)
        {
            this.messageRepository = messageRepository;
            this.queueRepository = queueRepository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<AppResponse<AppTaskResponse>> Handle(SaveEventBusReceivedMessageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return AppResponse<AppTaskResponse>.Custom(HttpStatusCode.BadRequest, "Invalid request!");

                var validationResponse = request.Validate();
                if (!validationResponse.IsSuccess)
                    return AppResponse<AppTaskResponse>.Copy(validationResponse);

                var queue = await queueRepository.GetById(request.QueueId);
                if (queue is null)
                    return AppResponse<AppTaskResponse>.Custom(HttpStatusCode.NotFound, $"Message {request.RequestId} not found!");

                var currentMessage = await messageRepository.GetById(request.RequestId);
                if (currentMessage is not null)
                    currentMessage.UpdateBasicData(request.CreatedAt, request.Type, Convert.ToString(request.Content));
                else
                    currentMessage = mapper.Map<Domain.Entities.EventBusReceivedMessage>(request);

                currentMessage.AddQueue(queue);

                var repositoryResponse = await messageRepository.Save(currentMessage);
                if (!repositoryResponse.IsSuccess)
                    return AppResponse<AppTaskResponse>.Copy(repositoryResponse);

                await mediator.Publish(EventLogNotification.Create(this, $"Event bus message {request.RequestId} saved!"));

                var responseContent = new List<AppTaskResponse> { new AppTaskResponse(request.RequestId) };
                return AppResponse<AppTaskResponse>.Success(responseContent);
            }
            catch (Exception ex)
            {
                await mediator.Publish(EventLogNotification.Create(this, ex,
                    $"An error occurred when saving event bus received message {request.RequestId}!"));
                return AppResponse<AppTaskResponse>.Error(ex);
            }
        }
    }
}
