using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Repositories;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Models;
using EventBusInbox.Tests.FakeData.Entities;
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

        public static Mock<IEventBusReceivedMessageRepository> DeleteEventBusReceivedMessageHandler_Success()
        {
            var mock = new Mock<IEventBusReceivedMessageRepository>();

            mock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(EventBusReceivedMessageFakeData.Build());

            mock.Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(AppResponse<object>.Success("test"));

            return mock;
        }

        public static Mock<IEventBusReceivedMessageRepository> DeleteEventBusReceivedMessageHandler_GetById_NotFound()
        {
            var mock = new Mock<IEventBusReceivedMessageRepository>();

            mock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((EventBusReceivedMessage)default);

            mock.Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(AppResponse<object>.Success("test"));

            return mock;
        }

        public static Mock<IEventBusReceivedMessageRepository> DeleteEventBusReceivedMessageHandler_DeletingError()
        {
            var mock = new Mock<IEventBusReceivedMessageRepository>();

            mock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(EventBusReceivedMessageFakeData.Build());

            mock.Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(AppResponse<object>.Error(new Exception("test")));

            return mock;
        }

        public static Mock<IEventBusReceivedMessageRepository> DeleteEventBusReceivedMessageHandler_Exception()
        {
            var mock = new Mock<IEventBusReceivedMessageRepository>();

            mock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("test"));

            mock.Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(AppResponse<object>.Success("test"));

            return mock;
        }

        public static Mock<IEventBusReceivedMessageRepository> GetEventBusReceivedMessageHandler_Success()
        {
            var mock = new Mock<IEventBusReceivedMessageRepository>();

            mock.Setup(x => x.GetResponse(It.IsAny<Guid>()))
                .ReturnsAsync(GetEventBusReceivedMessageResponseFakeData.Build());

            return mock;
        }

        public static Mock<IEventBusReceivedMessageRepository> GetEventBusReceivedMessageHandler_Exception()
        {
            var mock = new Mock<IEventBusReceivedMessageRepository>();

            mock.Setup(x => x.GetResponse(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("test"));

            return mock;
        }
    }
}
