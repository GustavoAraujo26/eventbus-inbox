using EventBusInbox.Domain.Handlers;
using EventBusInbox.Domain.Notifications;
using EventBusInbox.Domain.Repositories;
using EventBusInbox.Domain.Requests;
using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using MediatR;
using System.Net;

namespace EventBusInbox.Handlers.Contracts
{
    internal class SendMessageHandler : ISendMessageHandler
    {
        private readonly IEventBusQueueRepository queueRepository;
        private readonly IRabbitMqRepository rabbitMqRepository;
        private readonly IMediator mediator;

        public SendMessageHandler(IEventBusQueueRepository queueRepository, IRabbitMqRepository rabbitMqRepository, IMediator mediator)
        {
            this.queueRepository = queueRepository;
            this.rabbitMqRepository = rabbitMqRepository;
            this.mediator = mediator;
        }

        public async Task<AppResponse<AppTaskResponse>> Handle(SendMessageRequest request, CancellationToken cancellationToken)
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
                    return AppResponse<AppTaskResponse>.Custom(HttpStatusCode.NotFound, $"Queue {request.QueueId} not found!");

                Domain.Entities.EventBusReceivedMessage entity = Domain.Entities.EventBusReceivedMessage.Create(request.RequestId, 
                    request.CreatedAt, request.Type, Convert.ToString(request.Content));
                entity.AddQueue(queue);

                rabbitMqRepository.SendMessage(entity);

                await mediator.Publish(EventLogNotification.Create(this, $"Event bus message {request.RequestId} sent!"));

                var responseContent = new List<AppTaskResponse> { new AppTaskResponse(request.RequestId) };
                return AppResponse<AppTaskResponse>.Success(responseContent);
            }
            catch (Exception ex)
            {
                await mediator.Publish(EventLogNotification.Create(this, ex,
                    $"An error occurred when sending message {request.RequestId} to RabbitMQ!"));
                return AppResponse<AppTaskResponse>.Error(ex);
            }
        }
    }
}
