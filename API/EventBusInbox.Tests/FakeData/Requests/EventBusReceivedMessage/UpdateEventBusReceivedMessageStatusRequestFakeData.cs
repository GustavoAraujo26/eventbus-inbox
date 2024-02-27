using Bogus;
using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using System.Net;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage
{
    internal static class UpdateEventBusReceivedMessageStatusRequestFakeData
    {
        public static UpdateEventBusReceivedMessageStatusRequest BuildSuccess(HttpStatusCode statusCode) => 
            new UpdateEventBusReceivedMessageStatusRequest(Guid.NewGuid(), statusCode, statusCode.ToString());

        public static UpdateEventBusReceivedMessageStatusRequest BuildFailure() =>
            new UpdateEventBusReceivedMessageStatusRequest(Guid.Empty, new Faker().Random.Enum<HttpStatusCode>(), null);
    }
}
