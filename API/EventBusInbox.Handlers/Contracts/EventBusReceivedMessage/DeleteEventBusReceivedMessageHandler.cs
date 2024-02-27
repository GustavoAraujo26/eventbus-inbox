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
    internal class DeleteEventBusReceivedMessageHandler : IDeleteEventBusReceivedMessageHandler
    {
        private readonly IEventBusReceivedMessageRepository repository;
        private readonly IMediator mediator;

        public DeleteEventBusReceivedMessageHandler(IEventBusReceivedMessageRepository repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        public async Task<AppResponse<AppTaskResponse>> Handle(DeleteEventBusReceivedMessageRequest request, 
            CancellationToken cancellationToken)
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
                    return AppResponse<AppTaskResponse>.Custom(HttpStatusCode.NotFound, "Message not found!");

                var repositoryResponse = await repository.Delete(request.RequestId);
                if (!repositoryResponse.IsSuccess)
                    return AppResponse<AppTaskResponse>.Copy(repositoryResponse);

                await mediator.Publish(EventLogNotification.Create(this, $"Event bus message {request.RequestId} deleted!"));

                var responseContent = new List<AppTaskResponse> { new AppTaskResponse(request.RequestId) };
                return AppResponse<AppTaskResponse>.Success(responseContent);
            }
            catch(Exception ex)
            {
                await mediator.Publish(EventLogNotification.Create(this, ex,
                    $"An error occurred when deleting event bus received message {request.RequestId}!"));
                return AppResponse<AppTaskResponse>.Error(ex);
            }
        }
    }
}
