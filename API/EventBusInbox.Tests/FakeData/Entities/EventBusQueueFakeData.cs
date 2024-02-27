using Bogus;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Enums;

namespace EventBusInbox.Tests.FakeData.Entities
{
    internal static class EventBusQueueFakeData
    {
        public static EventBusQueue Build(Guid? id = null)
        {
            var faker = new Faker();

            return EventBusQueue.Create(id ?? Guid.NewGuid(), 
                faker.Commerce.ProductName().ToLowerInvariant(), 
                faker.Commerce.ProductDescription(), faker.Random.Enum<QueueStatus>(), 3);
        }
    }
}
