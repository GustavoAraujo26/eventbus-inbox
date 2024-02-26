using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Requests.EventBusQueues;

namespace EventBusInbox.TypeConverters.Requests
{
    internal class SaveEventBusQueueRequestTypeConverter : ITypeConverter<SaveEventBusQueueRequest, EventBusQueue>
    {
        public EventBusQueue Convert(SaveEventBusQueueRequest source, EventBusQueue destination, ResolutionContext context) =>
            EventBusQueue.Create(source.Id, source.Name, source.Description, source.Status, source.ProcessingAttempts);
    }
}
