using EventBusInbox.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Handlers.EventBusReceivedMessage;

namespace EventBusInbox.Tests.HandlerTests.EventBusReceivedMessage
{
    public class DeleteEventBusReceivedMessageHandlerTests
    {
        [Fact]
        public async Task ShouldReturnSuccess()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.DeleteEventBusReceivedMessageHandler_Success().Object);

            var handler = services.GetService<IDeleteEventBusReceivedMessageHandler>();

            var request = DeleteEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_NullRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.DeleteEventBusReceivedMessageHandler_Success().Object);

            var handler = services.GetService<IDeleteEventBusReceivedMessageHandler>();

            var response = await handler.Handle(null, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_InvalidRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.DeleteEventBusReceivedMessageHandler_Success().Object);

            var handler = services.GetService<IDeleteEventBusReceivedMessageHandler>();

            var request = DeleteEventBusReceivedMessageRequestFakeData.BuildFailure();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_MessageNotFound()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.DeleteEventBusReceivedMessageHandler_GetById_NotFound().Object);

            var handler = services.GetService<IDeleteEventBusReceivedMessageHandler>();

            var request = DeleteEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_DeletingError()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.DeleteEventBusReceivedMessageHandler_DeletingError().Object);

            var handler = services.GetService<IDeleteEventBusReceivedMessageHandler>();

            var request = DeleteEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_Exception()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.DeleteEventBusReceivedMessageHandler_Exception().Object);

            var handler = services.GetService<IDeleteEventBusReceivedMessageHandler>();

            var request = DeleteEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }
    }
}
