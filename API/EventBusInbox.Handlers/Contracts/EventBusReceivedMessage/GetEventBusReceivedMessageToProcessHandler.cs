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
    internal class GetEventBusReceivedMessageToProcessHandler : IGetEventBusReceivedMessageToProcessHandler
    {
        private readonly IEventBusReceivedMessageRepository repository;
        private readonly IMediator mediator;

        public GetEventBusReceivedMessageToProcessHandler(IEventBusReceivedMessageRepository repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        public async Task<AppResponse<GetEventBusReceivedMessageToProcessResponse>> Handle(GetEventBusReceivedMessageToProcessRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return AppResponse<GetEventBusReceivedMessageToProcessResponse>.Custom(HttpStatusCode.BadRequest, "Invalid request!");

                var validationResponse = request.Validate();
                if (!validationResponse.IsSuccess)
                    return AppResponse<GetEventBusReceivedMessageToProcessResponse>.Copy(validationResponse);

                var response = await repository.List(request);

                return AppResponse<GetEventBusReceivedMessageToProcessResponse>.Success(response);
            }
            catch (Exception ex)
            {
                await mediator.Publish(EventLogNotification.Create(this, ex,
                    $"An error occurred when retrieving event bus message list to process!"));
                return AppResponse<GetEventBusReceivedMessageToProcessResponse>.Error(ex);
            }
        }
    }
}
