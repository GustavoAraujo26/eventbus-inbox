using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Models;

namespace EventBusInbox.TypeConverters.Models
{
    internal class EventBusQueueModelTypeConverter : ITypeConverter<EventBusQueue, EventBusQueueModel>
    {
        public EventBusQueueModel Convert(EventBusQueue source, EventBusQueueModel destination, ResolutionContext context) =>
            new EventBusQueueModel
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Status = source.Status,
                ProcessingAttempts = source.ProcessingAttempts
            };
    }
}
