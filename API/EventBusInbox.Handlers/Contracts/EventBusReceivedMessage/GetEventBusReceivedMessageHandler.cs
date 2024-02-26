using EventBusInbox.Domain.Handlers.EventBusReceivedMessage;
using EventBusInbox.Domain.Notifications;
using EventBusInbox.Domain.Repositories;
using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Models;
using MediatR;
using System.Net;

namespace EventBusInbox.Handlers.Contracts.EventBusReceivedMessage
{
    internal class GetEventBusReceivedMessageHandler : IGetEventBusReceivedMessageHandler
    {
        private readonly IEventBusReceivedMessageRepository repository;
        private readonly IMediator mediator;

        public GetEventBusReceivedMessageHandler(IEventBusReceivedMessageRepository repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        public async Task<AppResponse<GetEventBusReceivedMessageResponse>> Handle(GetEventBusReceivedMessageRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return AppResponse<GetEventBusReceivedMessageResponse>.Custom(HttpStatusCode.BadRequest, "Invalid request!");

                var validationResponse = request.Validate();
                if (!validationResponse.IsSuccess)
                    return AppResponse<GetEventBusReceivedMessageResponse>.Copy(validationResponse);

                var response = await repository.GetResponse(request.RequestId);

                return AppResponse<GetEventBusReceivedMessageResponse>.Success(response);
            }
            catch (Exception ex)
            {
                await mediator.Publish(EventLogNotification.Create(this, ex,
                    $"An error occurred when retrieving event bus message {request.RequestId}!"));
                return AppResponse<GetEventBusReceivedMessageResponse>.Error(ex);
            }
        }
    }
}
