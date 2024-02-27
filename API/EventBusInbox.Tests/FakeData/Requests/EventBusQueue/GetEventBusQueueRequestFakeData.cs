using EventBusInbox.Domain.Requests.EventBusQueues;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusQueue
{
    internal static class GetEventBusQueueRequestFakeData
    {
        public static GetEventBusQueueRequest Build(bool summarizeMessages) =>
            new GetEventBusQueueRequest(Guid.NewGuid(), null, summarizeMessages);
    }
}
