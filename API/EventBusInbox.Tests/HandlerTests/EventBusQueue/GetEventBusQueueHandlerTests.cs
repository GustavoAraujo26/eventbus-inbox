﻿using EventBusInbox.Domain.Handlers.EventBusQueue;
using EventBusInbox.Tests.FakeData.Requests.EventBusQueue;
using EventBusInbox.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using EventBusInbox.Shared.Extensions;

namespace EventBusInbox.Tests.HandlerTests.EventBusQueue
{
    public class GetEventBusQueueHandlerTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ShouldReturnSuccess(bool summarize)
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.GetEventBusQueueHandler_Success().Object);
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.GetEventBusQueueHandler_Success().Object);

            var handler = services.GetService<IGetEventBusQueueHandler>();

            var request = GetEventBusQueueRequestFakeData.BuildSuccess(summarize);

            var response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_NullRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.GetEventBusQueueHandler_Success().Object);
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.GetEventBusQueueHandler_Success().Object);

            var handler = services.GetService<IGetEventBusQueueHandler>();

            var response = await handler.Handle(null, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_InvalidRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.GetEventBusQueueHandler_Success().Object);
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.GetEventBusQueueHandler_Success().Object);

            var handler = services.GetService<IGetEventBusQueueHandler>();

            var request = GetEventBusQueueRequestFakeData.BuildFailure();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_QueueNotFound()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.GetEventBusQueueHandler_Get_NotFound().Object);
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.GetEventBusQueueHandler_Success().Object);

            var handler = services.GetService<IGetEventBusQueueHandler>();

            var request = GetEventBusQueueRequestFakeData.BuildSuccess(true);

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_Exception()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusQueueRepositoryMock.GetEventBusQueueHandler_Exception().Object);
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock.GetEventBusQueueHandler_Success().Object);

            var handler = services.GetService<IGetEventBusQueueHandler>();

            var request = GetEventBusQueueRequestFakeData.BuildSuccess(true);

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }
    }
}
