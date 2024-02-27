using EventBusInbox.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Tests.FakeData.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Handlers.EventBusReceivedMessage;
using Bogus;
using System.Net;
using EventBusInbox.Domain.Enums;

namespace EventBusInbox.Tests.HandlerTests.EventBusReceivedMessage
{
    public class UpdateEventBusReceivedMessageStatusHandlerTests
    {
        [Fact]
        public async Task ShouldReturnSuccess()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .UpdateEventBusReceivedMessageStatusHandler_Success(EventBusMessageStatus.Pending).Object);

            var handler = services.GetService<IUpdateEventBusReceivedMessageStatusHandler>();

            var request = UpdateEventBusReceivedMessageStatusRequestFakeData.BuildSuccess(new Faker().Random.Enum<HttpStatusCode>());

            var response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_NullRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .UpdateEventBusReceivedMessageStatusHandler_Success(EventBusMessageStatus.Pending).Object);

            var handler = services.GetService<IUpdateEventBusReceivedMessageStatusHandler>();

            var response = await handler.Handle(null, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_InvalidRequest()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .UpdateEventBusReceivedMessageStatusHandler_Success(EventBusMessageStatus.Pending).Object);

            var handler = services.GetService<IUpdateEventBusReceivedMessageStatusHandler>();

            var request = UpdateEventBusReceivedMessageStatusRequestFakeData.BuildFailure();

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_MessageNotFound()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .UpdateEventBusReceivedMessageStatusHandler_GetById_NotFound().Object);

            var handler = services.GetService<IUpdateEventBusReceivedMessageStatusHandler>();

            var request = UpdateEventBusReceivedMessageStatusRequestFakeData.BuildSuccess(new Faker().Random.Enum<HttpStatusCode>());

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Theory]
        [InlineData(EventBusMessageStatus.Completed)]
        [InlineData(EventBusMessageStatus.PermanentFailure)]
        public async Task ShouldReturnFailure_For_WrongStatus(EventBusMessageStatus messageStatus)
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .UpdateEventBusReceivedMessageStatusHandler_Success(messageStatus).Object);

            var handler = services.GetService<IUpdateEventBusReceivedMessageStatusHandler>();

            var request = UpdateEventBusReceivedMessageStatusRequestFakeData.BuildSuccess(new Faker().Random.Enum<HttpStatusCode>());

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_SavingError()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .UpdateEventBusReceivedMessageStatusHandler_SavingError().Object);

            var handler = services.GetService<IUpdateEventBusReceivedMessageStatusHandler>();

            var request = UpdateEventBusReceivedMessageStatusRequestFakeData.BuildSuccess(new Faker().Random.Enum<HttpStatusCode>());

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task ShouldReturnFailure_For_Exception()
        {
            var services = EnvironmentConfig.BuildServices();
            services.AddTransient(obj => EventBusReceivedMessageRepositoryMock
                .UpdateEventBusReceivedMessageStatusHandler_Exception().Object);

            var handler = services.GetService<IUpdateEventBusReceivedMessageStatusHandler>();

            var request = UpdateEventBusReceivedMessageStatusRequestFakeData.BuildSuccess(new Faker().Random.Enum<HttpStatusCode>());

            var response = await handler.Handle(request, new CancellationToken());

            Assert.False(response.IsSuccess);
        }
    }
}
