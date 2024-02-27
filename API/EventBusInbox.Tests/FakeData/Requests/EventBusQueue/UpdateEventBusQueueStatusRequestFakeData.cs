using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Requests.EventBusQueues;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusQueue
{
    internal static class UpdateEventBusQueueStatusRequestFakeData
    {
        public static UpdateEventBusQueueStatusRequest Build(QueueStatus status) =>
            new UpdateEventBusQueueStatusRequest(Guid.NewGuid(), status);
    }
}
