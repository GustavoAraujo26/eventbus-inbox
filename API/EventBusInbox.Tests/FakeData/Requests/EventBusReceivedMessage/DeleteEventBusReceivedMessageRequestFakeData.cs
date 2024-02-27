using EventBusInbox.Domain.Requests.EventBusReceivedMessage;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage
{
    internal static class DeleteEventBusReceivedMessageRequestFakeData
    {
        public static DeleteEventBusReceivedMessageRequest BuildSuccess() =>
            new DeleteEventBusReceivedMessageRequest(Guid.NewGuid());

        public static DeleteEventBusReceivedMessageRequest BuildFailure() =>
            new DeleteEventBusReceivedMessageRequest(Guid.Empty);
    }
}
