using EventBusInbox.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Handlers.EventBusReceivedMessage;

namespace EventBusInbox.Tests.HandlerTests.EventBusReceivedMessage
{
    public class GetEventBusReceivedMessageHandlerTests
    {
        [Fact]
        public async Task ShouldReturnSuccess()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.GetEventBusReceivedMessageHandler_Success().Object);

            var handler = services.GetService<IGetEventBusReceivedMessageHandler>();

            var request = GetEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_NullRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.GetEventBusReceivedMessageHandler_Success().Object);

            var handler = services.GetService<IGetEventBusReceivedMessageHandler>();

            var response = await handler.Handle(null, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_InvalidRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.GetEventBusReceivedMessageHandler_Success().Object);

            var handler = services.GetService<IGetEventBusReceivedMessageHandler>();

            var request = GetEventBusReceivedMessageRequestFakeData.BuildFailure();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_Exception()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.GetEventBusReceivedMessageHandler_Exception().Object);

            var handler = services.GetService<IGetEventBusReceivedMessageHandler>();

            var request = GetEventBusReceivedMessageRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }
    }
}
