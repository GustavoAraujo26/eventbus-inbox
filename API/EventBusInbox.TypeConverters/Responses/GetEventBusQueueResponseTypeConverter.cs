using AutoMapper;
using EventBusInbox.Domain.Models;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Shared.Extensions;

namespace EventBusInbox.TypeConverters.Responses
{
    internal class GetEventBusQueueResponseTypeConverter : ITypeConverter<EventBusQueueModel, GetEventBusQueueResponse>
    {
        public GetEventBusQueueResponse Convert(EventBusQueueModel source, GetEventBusQueueResponse destination, ResolutionContext context) =>
            new GetEventBusQueueResponse
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Status = source.Status.GetData(),
                ProcessingAttempts = source.ProcessingAttempts
            };
    }
}
