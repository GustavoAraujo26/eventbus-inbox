using EventBusInbox.Domain.Requests.EventBusQueues;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusQueue
{
    internal static class DeleteEventBusQueueRequestFakeData
    {
        public static DeleteEventBusQueueRequest BuildSuccess() =>
            new DeleteEventBusQueueRequest(Guid.NewGuid());

        public static DeleteEventBusQueueRequest BuildFailure() =>
            new DeleteEventBusQueueRequest(Guid.Empty);
    }
}
