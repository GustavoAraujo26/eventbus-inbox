using EventBusInbox.Tests.Mock;
using Microsoft.Extensions.DependencyInjection;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Domain.Handlers;
using EventBusInbox.Tests.FakeData.Requests;
using EventBusInbox.Tests.Mocks;

namespace EventBusInbox.Tests.HandlerTests
{
    public class SendMessageHandlerTests
    {
        [Fact]
        public async Task ShouldReturnSuccess()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.SendMessageHandler_Success().Object);
            services.AddTransient(obj => RabbitMqRepositoryMock.SendMessageHandler_Success().Object);

            var handler = services.GetService<ISendMessageHandler>();

            var request = SendMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_NullRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.SendMessageHandler_Success().Object);
            services.AddTransient(obj => RabbitMqRepositoryMock.SendMessageHandler_Success().Object);

            var handler = services.GetService<ISendMessageHandler>();

            var response = await handler.Handle(null, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_InvalidRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.SendMessageHandler_Success().Object);
            services.AddTransient(obj => RabbitMqRepositoryMock.SendMessageHandler_Success().Object);

            var handler = services.GetService<ISendMessageHandler>();

            var request = SendMessageRequestFakeData.BuildFailure();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_QueueNotFound()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.SendMessageHandler_GetById_NotFound().Object);
            services.AddTransient(obj => RabbitMqRepositoryMock.SendMessageHandler_Success().Object);

            var handler = services.GetService<ISendMessageHandler>();

            var request = SendMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_Exception()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.SendMessageHandler_Success().Object);
            services.AddTransient(obj => RabbitMqRepositoryMock.SendMessageHandlerException().Object);

            var handler = services.GetService<ISendMessageHandler>();

            var request = SendMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }
    }
}
