using EventBusInbox.Domain.Handlers.EventBusReceivedMessage;
using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusInbox.Handlers.Contracts.EventBusReceivedMessage
{
    internal class ReactivateEventBusReceivedMessageHandler : IReactivateEventBusReceivedMessageHandler
    {
        public Task<AppResponse<AppTaskResponse>> Handle(ReactivateEventBusReceivedMessageRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
