using EventBusInbox.Domain.Requests.EventBusReceivedMessage;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage
{
    internal static class GetEventBusReceivedMessageListRequestFakeData
    {
        public static GetEventBusReceivedMessageListRequest BuildSuccess() =>
            new GetEventBusReceivedMessageListRequest
            {
                Page = 1,
                PageSize = 10
            };

        public static GetEventBusReceivedMessageListRequest BuildFailure() =>
            new GetEventBusReceivedMessageListRequest
            {
                Page = 0,
                PageSize = 0
            };
    }
}
