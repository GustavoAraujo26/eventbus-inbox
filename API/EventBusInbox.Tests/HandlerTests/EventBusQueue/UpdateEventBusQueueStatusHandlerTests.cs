using EventBusInbox.Domain.Handlers.EventBusQueue;
using EventBusInbox.Tests.FakeData.Requests.EventBusQueue;
using EventBusInbox.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Domain.Enums;
using Bogus;

namespace EventBusInbox.Tests.HandlerTests.EventBusQueue
{
    public class UpdateEventBusQueueStatusHandlerTests
    {
        [Theory]
        [InlineData(QueueStatus.Enabled)]
        [InlineData(QueueStatus.Disabled)]
        public async Task ShouldReturnSuccess(QueueStatus status)
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.UpdateEventBusQueueStatusHandler_Success().Object);

            var handler = services.GetService<IUpdateEventBusQueueStatusHandler>();

            var request = UpdateEventBusQueueStatusRequestFakeData.BuildSuccess(status);

            var response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_NullRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.UpdateEventBusQueueStatusHandler_Success().Object);

            var handler = services.GetService<IUpdateEventBusQueueStatusHandler>();

            var response = await handler.Handle(null, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_InvalidRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.UpdateEventBusQueueStatusHandler_Success().Object);

            var handler = services.GetService<IUpdateEventBusQueueStatusHandler>();

            var request = UpdateEventBusQueueStatusRequestFakeData.BuildFailure();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_QueueNotFound()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.UpdateEventBusQueueStatusHandler_GetById_NotFound().Object);

            var handler = services.GetService<IUpdateEventBusQueueStatusHandler>();

            var request = UpdateEventBusQueueStatusRequestFakeData.BuildSuccess(new Faker().Random.Enum<QueueStatus>());

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_SavingError()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.UpdateEventBusQueueStatusHandler_SavingFailure().Object);

            var handler = services.GetService<IUpdateEventBusQueueStatusHandler>();

            var request = UpdateEventBusQueueStatusRequestFakeData.BuildSuccess(new Faker().Random.Enum<QueueStatus>());

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_Exception()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.UpdateEventBusQueueStatusHandler_Exception().Object);

            var handler = services.GetService<IUpdateEventBusQueueStatusHandler>();

            var request = UpdateEventBusQueueStatusRequestFakeData.BuildSuccess(new Faker().Random.Enum<QueueStatus>());

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }
    }
}
