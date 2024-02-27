using EventBusInbox.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Handlers.EventBusReceivedMessage;
using EventBusInbox.Domain.Enums;

namespace EventBusInbox.Tests.HandlerTests.EventBusReceivedMessage
{
    public class ReactivateEventBusReceivedMessageHandlerTests
    {
        [Fact]
        public async Task ShouldReturnSuccess()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .ReactivateEventBusReceivedMessageHandler_Success(EventBusMessageStatus.Completed).Object);

            var handler = services.GetService<IReactivateEventBusReceivedMessageHandler>();

            var request = ReactivateEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_NullRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .ReactivateEventBusReceivedMessageHandler_Success(EventBusMessageStatus.Completed).Object);

            var handler = services.GetService<IReactivateEventBusReceivedMessageHandler>();

            var response = await handler.Handle(null, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_InvalidRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .ReactivateEventBusReceivedMessageHandler_Success(EventBusMessageStatus.Completed).Object);

            var handler = services.GetService<IReactivateEventBusReceivedMessageHandler>();

            var request = ReactivateEventBusReceivedMessageRequestFakeData.BuildFailure();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_MessageNotFound()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .ReactivateEventBusReceivedMessageHandler_GetById_NotFound().Object);

            var handler = services.GetService<IReactivateEventBusReceivedMessageHandler>();

            var request = ReactivateEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_InvalidStatus()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .ReactivateEventBusReceivedMessageHandler_Success(EventBusMessageStatus.Pending).Object);

            var handler = services.GetService<IReactivateEventBusReceivedMessageHandler>();

            var request = ReactivateEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_SavingError()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .ReactivateEventBusReceivedMessageHandler_SavingError(EventBusMessageStatus.Completed).Object);

            var handler = services.GetService<IReactivateEventBusReceivedMessageHandler>();

            var request = ReactivateEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_Exception()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .ReactivateEventBusReceivedMessageHandler_Exception().Object);

            var handler = services.GetService<IReactivateEventBusReceivedMessageHandler>();

            var request = ReactivateEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }
    }
}
