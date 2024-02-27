using EventBusInbox.Domain.Requests.EventBusReceivedMessage;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage
{
    internal static class GetEventBusReceivedMessageToProcessRequestFakeData
    {
        public static GetEventBusReceivedMessageToProcessRequest BuildSuccess() =>
            new GetEventBusReceivedMessageToProcessRequest(Guid.NewGuid(), 1, 10);

        public static GetEventBusReceivedMessageToProcessRequest BuildFailure() =>
            new GetEventBusReceivedMessageToProcessRequest(Guid.Empty, 0, 0);
    }
}
