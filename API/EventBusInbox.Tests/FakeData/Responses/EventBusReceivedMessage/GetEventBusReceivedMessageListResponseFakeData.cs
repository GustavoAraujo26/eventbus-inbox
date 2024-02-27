using Bogus;
using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Tests.FakeData.Responses.EventBusQueue;

namespace EventBusInbox.Tests.FakeData.Responses.EventBusReceivedMessage
{
    internal static class GetEventBusReceivedMessageListResponseFakeData
    {
        public static GetEventBusReceivedMessageListResponse BuildLine()
        {
            var faker = new Faker();

            var queue = GetEventBusQueueResponseFakeData.BuildLine(faker.Random.Enum<QueueStatus>());

            return new GetEventBusReceivedMessageListResponse
            {
                RequestId = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Type = "test",
                Queue = queue,
                Status = faker.Random.Enum<EventBusMessageStatus>().GetData(),
                ProcessingAttempts = 3,
                LastUpdate = ProcessingHistoryLineResponseFakeData.BuildLine()
            };
        }

        public static List<GetEventBusReceivedMessageListResponse> BuildList()
        {
            var result = new List<GetEventBusReceivedMessageListResponse>();

            var count = 1;
            while(count < 5)
            {
                result.Add(BuildLine());
                count++;
            }

            return result;
        }
    }
}
