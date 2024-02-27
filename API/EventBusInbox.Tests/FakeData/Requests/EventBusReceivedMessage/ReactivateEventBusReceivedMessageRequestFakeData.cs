using EventBusInbox.Domain.Requests.EventBusReceivedMessage;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage
{
    internal static class ReactivateEventBusReceivedMessageRequestFakeData
    {
        public static ReactivateEventBusReceivedMessageRequest BuildSuccess() =>
            new ReactivateEventBusReceivedMessageRequest(Guid.NewGuid());

        public static ReactivateEventBusReceivedMessageRequest BuildFailure() =>
            new ReactivateEventBusReceivedMessageRequest(Guid.Empty);
    }
}
