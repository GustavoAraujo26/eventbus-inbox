using EventBusInbox.Domain.Repositories;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Tests.FakeData.Responses.EventBusReceivedMessage;
using Moq;

namespace EventBusInbox.Tests.Mocks
{
    internal static class EventBusReceivedMessageRepositoryMock
    {
        public static Mock<IEventBusReceivedMessageRepository> GetEventBusQueueHandler_Success()
        {
            var mock = new Mock<IEventBusReceivedMessageRepository>();

            var summarizationList = SummarizeEventBusReceivedMessagesResponseFakeData.Build();
            var linkedList = summarizationList.Select(x => 
                new KeyValuePair<Guid, SummarizeEventBusReceivedMessagesResponse>(Guid.NewGuid(), x)).ToList();

            mock.Setup(x => x.Summarize(It.IsAny<List<Guid>>()))
                .ReturnsAsync(linkedList);

            return mock;
        }
    }
}
