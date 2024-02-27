using EventBusInbox.Domain.Requests.EventBusReceivedMessage;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage
{
    internal static class GetEventBusReceivedMessageRequestFakeData
    {
        public static GetEventBusReceivedMessageRequest BuildSuccess() =>
            new GetEventBusReceivedMessageRequest(Guid.NewGuid());

        public static GetEventBusReceivedMessageRequest BuildFailure() =>
            new GetEventBusReceivedMessageRequest(Guid.Empty);
    }
}
