using EventBusInbox.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Handlers.EventBusReceivedMessage;

namespace EventBusInbox.Tests.HandlerTests.EventBusReceivedMessage
{
    public class SaveEventBusReceivedMessageHandlerTests
    {
        [Fact]
        public async Task ShouldReturnSuccess_For_OldMessage()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.EventBusReceivedMessageHandler_Success().Object);
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.SaveEventBusReceivedMessageHandler_Success().Object);

            var handler = services.GetService<ISaveEventBusReceivedMessageHandler>();

            var request = SaveEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnSuccess_For_NewMessage()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.EventBusReceivedMessageHandler_Success().Object);
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.SaveEventBusReceivedMessageHandler_GetById_NotFound().Object);

            var handler = services.GetService<ISaveEventBusReceivedMessageHandler>();

            var request = SaveEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_NullRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.EventBusReceivedMessageHandler_Success().Object);
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.SaveEventBusReceivedMessageHandler_Success().Object);

            var handler = services.GetService<ISaveEventBusReceivedMessageHandler>();

            var response = await handler.Handle(null, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_InvalidRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.EventBusReceivedMessageHandler_Success().Object);
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.SaveEventBusReceivedMessageHandler_Success().Object);

            var handler = services.GetService<ISaveEventBusReceivedMessageHandler>();

            var request = SaveEventBusReceivedMessageRequestFakeData.BuildFailure();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_QueueNotFound()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.EventBusReceivedMessageHandler_GetById_NotFound().Object);
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.SaveEventBusReceivedMessageHandler_Success().Object);

            var handler = services.GetService<ISaveEventBusReceivedMessageHandler>();

            var request = SaveEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_SavingError()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.EventBusReceivedMessageHandler_Success().Object);
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.SaveEventBusReceivedMessageHandler_SavingError().Object);

            var handler = services.GetService<ISaveEventBusReceivedMessageHandler>();

            var request = SaveEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_Exception()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.EventBusReceivedMessageHandler_Success().Object);
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.SaveEventBusReceivedMessageHandler_Exception().Object);

            var handler = services.GetService<ISaveEventBusReceivedMessageHandler>();

            var request = SaveEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }
    }
}
