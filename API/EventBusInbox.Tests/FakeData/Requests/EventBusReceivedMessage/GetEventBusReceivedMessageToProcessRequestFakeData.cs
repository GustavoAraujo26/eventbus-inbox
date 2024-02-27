using EventBusInbox.Domain.Requests.EventBusReceivedMessage;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage
{
    internal static class GetEventBusReceivedMessageToProcessRequestFakeData
    {
        public static GetEventBusReceivedMessageToProcessRequest Build() =>
            new GetEventBusReceivedMessageToProcessRequest(Guid.NewGuid(), 1, 10);
    }
}
