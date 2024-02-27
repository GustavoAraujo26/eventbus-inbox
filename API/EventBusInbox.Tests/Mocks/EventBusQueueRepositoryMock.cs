using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Repositories;
using EventBusInbox.Shared.Models;
using EventBusInbox.Tests.FakeData.Entities;
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
    }
}
