using Bogus;
using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Requests.EventBusQueues;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusQueue
{
    internal static class SaveEventBusQueueRequestFakeData
    {
        public static SaveEventBusQueueRequest Build(QueueStatus status)
        {
            var faker = new Faker();

            return new SaveEventBusQueueRequest(Guid.NewGuid(), 
                faker.Commerce.ProductName().ToLowerInvariant(), faker.Commerce.ProductDescription(), status, 3);
        }
    }
}
