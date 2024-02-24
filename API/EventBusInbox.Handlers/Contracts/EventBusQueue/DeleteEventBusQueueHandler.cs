using EventBusInbox.Domain.Handlers.EventBusQueue;
using EventBusInbox.Domain.Requests.EventBusQueues;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusInbox.Handlers.Contracts.EventBusQueue
{
    public class DeleteEventBusQueueHandler : IDeleteEventBusQueueHandler
    {
        public async Task<AppResponse<EventBusQueueResponse>> Handle(DeleteEventBusQueueRequest request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
