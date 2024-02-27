using EventBusInbox.Domain.Requests;

namespace EventBusInbox.Tests.FakeData.Requests
{
    internal static class SendMessageRequestFakeData
    {
        public static SendMessageRequest BuildSuccess()
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

            return new SendMessageRequest(Guid.NewGuid(), DateTime.Now, "test", messageContent, Guid.NewGuid());
        }

        public static SendMessageRequest BuildFailure() =>
            new SendMessageRequest(Guid.Empty, DateTime.MinValue, null, null, Guid.Empty);
    }
}
