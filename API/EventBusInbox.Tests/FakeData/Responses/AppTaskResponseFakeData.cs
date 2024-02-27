using EventBusInbox.Domain.Responses;

namespace EventBusInbox.Tests.FakeData.Responses
{
    internal static class AppTaskResponseFakeData
    {
        public static AppTaskResponse Build() => 
            new AppTaskResponse(Guid.NewGuid(), DateTime.Now);
    }
}
