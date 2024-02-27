using Bogus;
using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Extensions;

namespace EventBusInbox.Tests.FakeData.Responses.EventBusReceivedMessage
{
    internal static class SummarizeEventBusReceivedMessagesResponseFakeData
    {
        public static List<SummarizeEventBusReceivedMessagesResponse> Build()
        {
            var faker = new Faker();

            List<SummarizeEventBusReceivedMessagesResponse> result = new List<SummarizeEventBusReceivedMessagesResponse>();

            EventBusMessageStatus.Completed.List<EventBusMessageStatus>().ForEach(x => 
                result.Add(new SummarizeEventBusReceivedMessagesResponse(x, faker.Random.Number(1000))));

            return result;
        }
    }
}
