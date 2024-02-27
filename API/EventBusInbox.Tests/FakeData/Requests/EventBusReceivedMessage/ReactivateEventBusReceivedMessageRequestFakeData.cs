using EventBusInbox.Domain.Requests.EventBusReceivedMessage;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage
{
    internal static class ReactivateEventBusReceivedMessageRequestFakeData
    {
        public static ReactivateEventBusReceivedMessageRequest Build() =>
            new ReactivateEventBusReceivedMessageRequest(Guid.NewGuid());
    }
}
