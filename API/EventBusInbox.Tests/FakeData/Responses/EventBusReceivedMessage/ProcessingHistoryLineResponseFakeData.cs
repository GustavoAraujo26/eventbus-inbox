using Bogus;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Extensions;
using System.Net;

namespace EventBusInbox.Tests.FakeData.Responses.EventBusReceivedMessage
{
    internal static class ProcessingHistoryLineResponseFakeData
    {
        public static ProcessingHistoryLineResponse BuildLine()
        {
            var faker = new Faker();

            return new ProcessingHistoryLineResponse(DateTime.Now, faker.Random.Enum<HttpStatusCode>().GetData(), faker.Commerce.ProductMaterial());
        }

        public static List<ProcessingHistoryLineResponse> BuildList()
        {
            var result = new List<ProcessingHistoryLineResponse>();

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
