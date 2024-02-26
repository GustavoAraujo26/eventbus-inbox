using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Models;

namespace EventBusInbox.TypeConverters.Entities
{
    internal class EventBusReceivedMessageTypeConverter : ITypeConverter<EventBusReceivedMessageModel, EventBusReceivedMessage>
    {
        public EventBusReceivedMessage Convert(EventBusReceivedMessageModel source, EventBusReceivedMessage destination, ResolutionContext context) =>
            new EventBusReceivedMessage(
                source.RequestId, 
                source.CreatedAt,
                source.Type,
                source.Content,
                context.Mapper.Map<EventBusQueue>(source.Queue),
                source.Status,
                source.ProcessingAttempts,
                context.Mapper.Map<IList<ProcessingHistoryLine>>(source.ProcessingHistory)
            );
    }
}
