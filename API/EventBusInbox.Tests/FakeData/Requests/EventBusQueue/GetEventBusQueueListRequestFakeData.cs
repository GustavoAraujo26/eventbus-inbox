using EventBusInbox.Domain.Requests.EventBusQueues;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusQueue
{
    internal static class GetEventBusQueueListRequestFakeData
    {
        public static GetEventBusQueueListRequest BuildSuccess(bool summarizeMessages) =>
            new GetEventBusQueueListRequest
            {
                Page = 1,
                PageSize = 10,
                SummarizeMessages = summarizeMessages,
            };

        public static GetEventBusQueueListRequest BuildFailure() =>
            new GetEventBusQueueListRequest
            {
                Page = 0,
                PageSize = 0,
                SummarizeMessages = false,
            };
    }
}
