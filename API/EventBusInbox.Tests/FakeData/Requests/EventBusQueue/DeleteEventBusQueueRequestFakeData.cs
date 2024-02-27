using Bogus;
using EventBusInbox.Domain.Requests.EventBusQueues;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusQueue
{
    internal static class DeleteEventBusQueueRequestFakeData
    {
        public static DeleteEventBusQueueRequest Build() =>
            new DeleteEventBusQueueRequest(new Faker().Random.Guid());
    }
}
