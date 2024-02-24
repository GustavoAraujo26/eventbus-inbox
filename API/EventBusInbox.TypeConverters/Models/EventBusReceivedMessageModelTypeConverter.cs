using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Models;

namespace EventBusInbox.TypeConverters.Models
{
    internal class EventBusReceivedMessageModelTypeConverter : ITypeConverter<EventBusReceivedMessage, EventBusReceivedMessageModel>
    {
        public EventBusReceivedMessageModel Convert(EventBusReceivedMessage source, EventBusReceivedMessageModel destination, ResolutionContext context) =>
            new EventBusReceivedMessageModel
            {
                RequestId = source.RequestId,
                CreatedAt = source.CreatedAt,
                Type = source.Type,
                Data = source.Data,
                Queue = context.Mapper.Map<EventBusQueueModel>(source.Queue),
                Status = source.Status,
                ProcessingAttempts = source.ProcessingAttempts,
                ProcessingHistory = context.Mapper.Map<List<ProcessingHistoryLineModel>>(source.ProcessingHistory)
            };
    }
}
