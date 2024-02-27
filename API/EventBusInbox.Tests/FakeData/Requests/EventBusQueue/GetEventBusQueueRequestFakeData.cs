using EventBusInbox.Domain.Requests.EventBusQueues;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusQueue
{
    internal static class GetEventBusQueueRequestFakeData
    {
        public static GetEventBusQueueRequest BuildSuccess(bool summarizeMessages) =>
            new GetEventBusQueueRequest(Guid.NewGuid(), null, summarizeMessages);

        public static GetEventBusQueueRequest BuildFailure() =>
            new GetEventBusQueueRequest(Guid.Empty, null, false);
    }
}
