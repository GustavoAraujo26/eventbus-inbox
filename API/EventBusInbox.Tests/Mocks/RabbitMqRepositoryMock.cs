using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Repositories;
using Moq;

namespace EventBusInbox.Tests.Mock
{
    internal static class RabbitMqRepositoryMock
    {
        public static Mock<IRabbitMqRepository> SendMessageHandler_Success()
        {
            var mock = new Mock<IRabbitMqRepository>();

            mock.Setup(x => x.SendMessage(It.IsAny<EventBusReceivedMessage>()));

            return mock;
        }

        public static Mock<IRabbitMqRepository> SendMessageHandlerException()
        {
            var mock = new Mock<IRabbitMqRepository>();

            mock.Setup(x => x.SendMessage(It.IsAny<EventBusReceivedMessage>()))
                .Throws(new Exception("test"));

            return mock;
        }
    }
}
