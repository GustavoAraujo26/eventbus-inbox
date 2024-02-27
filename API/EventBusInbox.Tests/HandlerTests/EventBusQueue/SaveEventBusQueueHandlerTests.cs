using EventBusInbox.Domain.Handlers.EventBusQueue;
using EventBusInbox.Tests.FakeData.Requests.EventBusQueue;
using Microsoft.Extensions.DependencyInjection;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Tests.Mocks;

namespace EventBusInbox.Tests.HandlerTests.EventBusQueue
{
    public class SaveEventBusQueueHandlerTests
    {
        [Fact]
        public async Task ShouldReturnSuccess()
        {
            var currentId = Guid.NewGuid();

            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.SaveEventBusQueueHandler_Success(currentId).Object);

            var handler = services.GetService<ISaveEventBusQueueHandler>();

            var request = SaveEventBusQueueRequestFakeData.BuildSuccess(currentId);

            var response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_NullRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.SaveEventBusQueueHandler_Success().Object);

            var handler = services.GetService<ISaveEventBusQueueHandler>();

            var response = await handler.Handle(null, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_InvalidRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.SaveEventBusQueueHandler_Success().Object);

            var handler = services.GetService<ISaveEventBusQueueHandler>();

            var request = SaveEventBusQueueRequestFakeData.BuildFailure();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_QueueNotFound()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.SaveEventBusQueueHandler_Success().Object);

            var handler = services.GetService<ISaveEventBusQueueHandler>();

            var request = SaveEventBusQueueRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_SavingError()
        {
            var currentId = Guid.NewGuid();

            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.SaveEventBusQueueHandler_SavingFailure(currentId).Object);

            var handler = services.GetService<ISaveEventBusQueueHandler>();

            var request = SaveEventBusQueueRequestFakeData.BuildSuccess(currentId);

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_Exception()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.SaveEventBusQueueHandler_Exception().Object);

            var handler = services.GetService<ISaveEventBusQueueHandler>();

            var request = SaveEventBusQueueRequestFakeData.BuildSuccess();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }
    }
}
