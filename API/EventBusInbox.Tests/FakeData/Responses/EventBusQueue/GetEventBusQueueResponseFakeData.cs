using Bogus;
using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Tests.FakeData.Responses.EventBusReceivedMessage;

namespace EventBusInbox.Tests.FakeData.Responses.EventBusQueue
{
    internal static class GetEventBusQueueResponseFakeData
    {
        public static GetEventBusQueueResponse BuildLine(QueueStatus status)
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

        public static List<GetEventBusQueueResponse> BuildList()
        {
            var faker = new Faker();

            var result = new List<GetEventBusQueueResponse>();

            var count = 1;
            while(count < 5)
            {
                result.Add(BuildLine(faker.Random.Enum<QueueStatus>()));
                count++;
            }

            return result;
        }
    }
}
