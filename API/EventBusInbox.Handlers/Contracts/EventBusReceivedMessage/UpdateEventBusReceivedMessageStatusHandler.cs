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
    internal class UpdateEventBusReceivedMessageStatusHandler : IUpdateEventBusReceivedMessageStatusHandler
    {
        private readonly IEventBusReceivedMessageRepository repository;
        private readonly IMediator mediator;

        public UpdateEventBusReceivedMessageStatusHandler(IEventBusReceivedMessageRepository repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        public async Task<AppResponse<AppTaskResponse>> Handle(UpdateEventBusReceivedMessageStatusRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return AppResponse<AppTaskResponse>.Custom(HttpStatusCode.BadRequest, "Invalid request!");

                var validationResponse = request.Validate();
                if (!validationResponse.IsSuccess)
                    return AppResponse<AppTaskResponse>.Copy(validationResponse);

                var currentMessage = await repository.GetById(request.RequestId);
                if (currentMessage is null)
                    return AppResponse<AppTaskResponse>.Custom(HttpStatusCode.NotFound, $"Message {request.RequestId} not found!");

                currentMessage.SetResult(request.ProcessStatus, request.ResultMessage);

                var repositoryResponse = await repository.Save(currentMessage);
                if (!repositoryResponse.IsSuccess)
                    return AppResponse<AppTaskResponse>.Copy(repositoryResponse);

                await mediator.Publish(EventLogNotification.Create(this, $"Event bus message {request.RequestId} updated!"));

                var responseContent = new List<AppTaskResponse> { new AppTaskResponse(request.RequestId) };
                return AppResponse<AppTaskResponse>.Success(responseContent);
            }
            catch (Exception ex)
            {
                await mediator.Publish(EventLogNotification.Create(this, ex,
                    $"An error occurred when update event bus received message status {request.RequestId}!"));
                return AppResponse<AppTaskResponse>.Error(ex);
            }
        }
    }
}
