using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Repositories;
using EventBusInbox.Domain.Requests.EventBusQueues;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Shared.Models;
using EventBusInbox.Tests.FakeData.Entities;
using EventBusInbox.Tests.FakeData.Responses.EventBusQueue;
using Moq;

namespace EventBusInbox.Tests.Mocks
{
    internal static class EventBusQueueRepositoryMock
    {
        public static Mock<IEventBusQueueRepository> DeleteEventBusQueueHandler_Success()
        {
            var mock = new Mock<IEventBusQueueRepository>();

            mock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(EventBusQueueFakeData.Build());

            mock.Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(AppResponse<object>.Success("test"));

            return mock;
        }

        public static Mock<IEventBusQueueRepository> DeleteEventBusQueueHandler_Exception()
        {
            var mock = new Mock<IEventBusQueueRepository>();

            var ex = new Exception("test");

            mock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .ThrowsAsync(ex);

            mock.Setup(x => x.Delete(It.IsAny<Guid>()))
                .ThrowsAsync(ex);

            return mock;
        }

        public static Mock<IEventBusQueueRepository> DeleteEventBusQueueHandler_GetById_NotFound()
        {
            var mock = new Mock<IEventBusQueueRepository>();

            mock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((EventBusQueue)null);

            mock.Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(AppResponse<object>.Success("test"));

            return mock;
        }

        public static Mock<IEventBusQueueRepository> DeleteEventBusQueueHandler_ErrorOnDelete()
        {
            var mock = new Mock<IEventBusQueueRepository>();

            mock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(EventBusQueueFakeData.Build());

            mock.Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(AppResponse<object>.Error(new Exception("test")));

            return mock;
        }

        public static Mock<IEventBusQueueRepository> GetEventBusQueueHandler_Success()
        {
            var mock = new Mock<IEventBusQueueRepository>();

            mock.Setup(x => x.Get(It.IsAny<GetEventBusQueueRequest>()))
                .ReturnsAsync(GetEventBusQueueResponseFakeData.BuildLine(It.IsAny<QueueStatus>()));

            return mock;
        }

        public static Mock<IEventBusQueueRepository> GetEventBusQueueHandler_Get_NotFound()
        {
            var mock = new Mock<IEventBusQueueRepository>();

            mock.Setup(x => x.Get(It.IsAny<GetEventBusQueueRequest>()))
                .ReturnsAsync((GetEventBusQueueResponse)null);

            return mock;
        }

        public static Mock<IEventBusQueueRepository> GetEventBusQueueHandler_Exception()
        {
            var mock = new Mock<IEventBusQueueRepository>();

            mock.Setup(x => x.Get(It.IsAny<GetEventBusQueueRequest>()))
                .ThrowsAsync(new Exception("test"));

            return mock;
        }

        public static Mock<IEventBusQueueRepository> GetEventBusQueueListHandler_Success()
        {
            var mock = new Mock<IEventBusQueueRepository>();

            mock.Setup(x => x.List(It.IsAny<GetEventBusQueueListRequest>()))
                .ReturnsAsync(GetEventBusQueueResponseFakeData.BuildList());

            return mock;
        }

        public static Mock<IEventBusQueueRepository> GetEventBusQueueListHandler_Exception()
        {
            var mock = new Mock<IEventBusQueueRepository>();

            mock.Setup(x => x.List(It.IsAny<GetEventBusQueueListRequest>()))
                .ThrowsAsync(new Exception("test"));

            return mock;
        }

        public static Mock<IEventBusQueueRepository> SaveEventBusQueueHandler_Success(Guid? id = null)
        {
            var mock = new Mock<IEventBusQueueRepository>();

            mock.Setup(x => x.GetByName(It.IsAny<string>()))
                .ReturnsAsync(EventBusQueueFakeData.Build(id));

            mock.Setup(x => x.Save(It.IsAny<EventBusQueue>()))
                .ReturnsAsync(AppResponse<object>.Success("test"));

            return mock;
        }

        public static Mock<IEventBusQueueRepository> SaveEventBusQueueHandler_SavingFailure(Guid? id = null)
        {
            var mock = new Mock<IEventBusQueueRepository>();

            mock.Setup(x => x.GetByName(It.IsAny<string>()))
                .ReturnsAsync(EventBusQueueFakeData.Build(id));

            mock.Setup(x => x.Save(It.IsAny<EventBusQueue>()))
                .ReturnsAsync(AppResponse<object>.Error(new Exception("test")));

            return mock;
        }

        public static Mock<IEventBusQueueRepository> SaveEventBusQueueHandler_Exception()
        {
            var mock = new Mock<IEventBusQueueRepository>();

            mock.Setup(x => x.GetByName(It.IsAny<string>()))
                .ThrowsAsync(new Exception("test"));

            mock.Setup(x => x.Save(It.IsAny<EventBusQueue>()))
                .ReturnsAsync(AppResponse<object>.Success("test"));

            return mock;
        }
    }
}
