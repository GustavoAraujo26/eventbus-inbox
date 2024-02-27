using EventBusInbox.Domain.Requests.EventBusReceivedMessage;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage
{
    internal static class DeleteEventBusReceivedMessageRequestFakeData
    {
        public static DeleteEventBusReceivedMessageRequest Build() =>
            new DeleteEventBusReceivedMessageRequest(Guid.NewGuid());
    }
}
