using EventBusInbox.Domain.Requests.EventBusReceivedMessage;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage
{
    internal static class GetEventBusReceivedMessageListRequestFakeData
    {
        public static GetEventBusReceivedMessageListRequest Build() =>
            new GetEventBusReceivedMessageListRequest
            {
                Page = 1,
                PageSize = 10
            };
    }
}
