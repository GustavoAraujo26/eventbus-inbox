using EventBusInbox.Domain.Requests.EventBusReceivedMessage;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage
{
    internal static class GetEventBusReceivedMessageRequestFakeData
    {
        public static GetEventBusReceivedMessageRequest Build() =>
            new GetEventBusReceivedMessageRequest(Guid.NewGuid());
    }
}
