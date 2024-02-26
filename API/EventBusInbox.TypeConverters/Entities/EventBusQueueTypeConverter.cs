using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Models;

namespace EventBusInbox.TypeConverters.Entities
{
    internal class EventBusQueueTypeConverter : ITypeConverter<EventBusQueueModel, EventBusQueue>
    {
        public EventBusQueue Convert(EventBusQueueModel source, EventBusQueue destination, ResolutionContext context) =>
            EventBusQueue.Create(source.Id, source.Name, source.Description, source.Status, source.ProcessingAttempts);
    }
}
