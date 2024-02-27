using Bogus;
using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Tests.FakeData.Responses.EventBusReceivedMessage;

namespace EventBusInbox.Tests.FakeData.Responses.EventBusQueue
{
    internal static class GetEventBusQueueResponseFakeData
    {
        public static GetEventBusQueueResponse Build(QueueStatus status)
        {
            var faker = new Faker();

            return new GetEventBusQueueResponse
            {
                Id = Guid.NewGuid(),
                Name = faker.Commerce.ProductName().ToLowerInvariant(),
                Description = faker.Commerce.ProductDescription(),
                Status = status.GetData(),
                ProcessingAttempts = 3,
                MessagesSummarization = SummarizeEventBusReceivedMessagesResponseFakeData.Build()
            };
        }
    }
}
