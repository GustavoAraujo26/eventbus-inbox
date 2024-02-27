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
    }
}
