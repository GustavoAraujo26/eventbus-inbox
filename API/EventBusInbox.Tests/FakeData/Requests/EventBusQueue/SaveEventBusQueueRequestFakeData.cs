using Bogus;
using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Requests.EventBusQueues;

namespace EventBusInbox.Tests.FakeData.Requests.EventBusQueue
{
    internal static class SaveEventBusQueueRequestFakeData
    {
        public static SaveEventBusQueueRequest BuildSuccess(Guid? id = null)
        {
            var faker = new Faker();

            return new SaveEventBusQueueRequest(id ?? Guid.NewGuid(), 
                faker.Commerce.ProductName().ToLowerInvariant(), 
                faker.Commerce.ProductDescription().Substring(0, 100), 
                faker.Random.Enum<QueueStatus>(), 3);
        }
        public static SaveEventBusQueueRequest BuildFailure()
        {
            var faker = new Faker();

            return new SaveEventBusQueueRequest(Guid.Empty,
                string.Empty, faker.Commerce.ProductDescription(), 
                faker.Random.Enum<QueueStatus>(), 0);
        }
    }
}
