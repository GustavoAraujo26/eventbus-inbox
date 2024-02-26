using EventBusInbox.Domain.Handlers.EventBusReceivedMessage;
using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusInbox.Handlers.Contracts.EventBusReceivedMessage
{
    internal class GetEventBusReceivedMessageListHandler : IGetEventBusReceivedMessageListHandler
    {
        public Task<AppResponse<GetEventBusReceivedMessageListResponse>> Handle(GetEventBusReceivedMessageListRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
