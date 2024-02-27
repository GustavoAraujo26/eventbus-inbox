using EventBusInbox.Domain.Requests.EventBusQueues;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusQueue
{
    internal static class GetEventBusQueueListRequestFakeData
    {
        public static GetEventBusQueueListRequest Build(bool summarizeMessages) =>
            new GetEventBusQueueListRequest
            {
                Page = 1,
                PageSize = 10,
                SummarizeMessages = summarizeMessages,
            };
    }
}
