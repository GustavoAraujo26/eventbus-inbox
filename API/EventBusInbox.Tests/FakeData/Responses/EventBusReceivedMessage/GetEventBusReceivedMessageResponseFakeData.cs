using Bogus;
using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Tests.FakeData.Responses.EventBusQueue;

namespace EventBusInbox.Tests.FakeData.Responses.EventBusReceivedMessage
{
    internal static class GetEventBusReceivedMessageResponseFakeData
    {
        public static GetEventBusReceivedMessageResponse Build()
        {
            var faker = new Faker();

            var queue = GetEventBusQueueResponseFakeData.Build(faker.Random.Enum<QueueStatus>());

            var messageContent = new
            {
                Prop1 = 1,
                Prop2 = DateTime.Now,
                Prop3 = "Test",
                Prop4 = new object[]
                {
                    new { Prop1 = "Test1", Prop2 = "Test1", Prop3 = "Test1" },
                    new { Prop1 = "Test2", Prop2 = "Test2", Prop3 = "Test2" },
                    new { Prop1 = "Test3", Prop2 = "Test3", Prop3 = "Test3" },
                }
            };

            return new GetEventBusReceivedMessageResponse
            {
                RequestId = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Type = "test",
                Content = messageContent,
                Queue = queue,
                Status = faker.Random.Enum<EventBusMessageStatus>().GetData(),
                ProcessingAttempts = 1,
                ProcessingHistory = ProcessingHistoryLineResponseFakeData.BuildList()
            };
        }
    }
}
