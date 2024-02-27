using EventBusInbox.Domain.Handlers.EventBusQueue;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Tests.FakeData.Requests.EventBusQueue;
using EventBusInbox.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace EventBusInbox.Tests.HandlerTests.EventBusQueue
{
    public class DeleteEventBusQueueHandlerTests
    {
        [Fact]
        public async Task ShouldReturnSuccess()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.DeleteEventBusQueueHandler_Success().Object);

            var handler = services.GetService<IDeleteEventBusQueueHandler>();

            var request = DeleteEventBusQueueRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_NullRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.DeleteEventBusQueueHandler_Success().Object);

            var handler = services.GetService<IDeleteEventBusQueueHandler>();

            var response = await handler.Handle(null, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_InvalidRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.DeleteEventBusQueueHandler_Success().Object);

            var handler = services.GetService<IDeleteEventBusQueueHandler>();

            var request = DeleteEventBusQueueRequestFakeData.BuildFailure();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_Exception()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.DeleteEventBusQueueHandler_Exception().Object);

            var handler = services.GetService<IDeleteEventBusQueueHandler>();

            var request = DeleteEventBusQueueRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_QueueNotFound()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.DeleteEventBusQueueHandler_GetById_NotFound().Object);

            var handler = services.GetService<IDeleteEventBusQueueHandler>();

            var request = DeleteEventBusQueueRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_ErrorOnDelete()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.DeleteEventBusQueueHandler_ErrorOnDelete().Object);

            var handler = services.GetService<IDeleteEventBusQueueHandler>();

            var request = DeleteEventBusQueueRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }
    }
}
