using Bogus;
using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Requests.EventBusQueues;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusQueue
{
    internal static class UpdateEventBusQueueStatusRequestFakeData
    {
        public static UpdateEventBusQueueStatusRequest BuildSuccess(QueueStatus status) =>
            new UpdateEventBusQueueStatusRequest(Guid.NewGuid(), status);

        public static UpdateEventBusQueueStatusRequest BuildFailure() =>
            new UpdateEventBusQueueStatusRequest(Guid.Empty, new Faker().Random.Enum<QueueStatus>());
    }
}
