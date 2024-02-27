using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using System.Net;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage
{
    internal static class UpdateEventBusReceivedMessageStatusRequestFakeData
    {
        public static UpdateEventBusReceivedMessageStatusRequest Build(HttpStatusCode statusCode) => 
            new UpdateEventBusReceivedMessageStatusRequest(Guid.NewGuid(), statusCode, statusCode.ToString());
    }
}
