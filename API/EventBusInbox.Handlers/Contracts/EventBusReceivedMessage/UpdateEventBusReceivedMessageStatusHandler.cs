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
    internal class UpdateEventBusReceivedMessageStatusHandler : IUpdateEventBusReceivedMessageStatusHandler
    {
        public Task<AppResponse<AppTaskResponse>> Handle(UpdateEventBusReceivedMessageStatusRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
