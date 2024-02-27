using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Enums;
using Newtonsoft.Json;
using System.Net;

namespace EventBusInbox.Tests.FakeData.Entities
{
    internal static class EventBusReceivedMessageFakeData
    {
        public static EventBusReceivedMessage Build(EventBusMessageStatus? status = null)
        {
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

            var jsonContent = JsonConvert.SerializeObject(messageContent);

            var message = EventBusReceivedMessage.Create(Guid.NewGuid(), DateTime.Now, "test", jsonContent);
            if (status is null)
                return message;

            message.AddQueue(EventBusQueueFakeData.Build());

            switch (status)
            {
                case EventBusMessageStatus.Pending:
                    return message;
                case EventBusMessageStatus.Completed:
                    message.SetResult(HttpStatusCode.OK, "test");
                    return message;
                case EventBusMessageStatus.TemporaryFailure:
                    message.SetResult(HttpStatusCode.InternalServerError, "test");
                    return message;
                case EventBusMessageStatus.PermanentFailure:
                    message.SetResult(HttpStatusCode.BadRequest, "test");
                    return message;
                default:
                    return message;
            }
        }
    }
}
