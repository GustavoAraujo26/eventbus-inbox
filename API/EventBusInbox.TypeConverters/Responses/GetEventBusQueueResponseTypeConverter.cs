using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Responses.EventBusQueues;

namespace EventBusInbox.TypeConverters.Responses
{
    internal class GetEventBusQueueResponseTypeConverter : ITypeConverter<EventBusQueue, GetEventBusQueueResponse>
    {
        public GetEventBusQueueResponse Convert(EventBusQueue source, GetEventBusQueueResponse destination, ResolutionContext context) =>
            new GetEventBusQueueResponse
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Status = source.Status,
                ProcessingAttempts = source.ProcessingAttempts
            };
    }
}
