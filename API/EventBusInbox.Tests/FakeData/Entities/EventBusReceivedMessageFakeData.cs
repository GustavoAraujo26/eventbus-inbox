using EventBusInbox.Domain.Entities;
using Newtonsoft.Json;

namespace EventBusInbox.Tests.FakeData.Entities
{
    internal static class EventBusReceivedMessageFakeData
    {
        public static EventBusReceivedMessage Build()
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

            return EventBusReceivedMessage.Create(Guid.NewGuid(), DateTime.Now, "test", jsonContent);
        }
    }
}
