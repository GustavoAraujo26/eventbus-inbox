using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Models;
using EventBusInbox.TypeConverters.Entities;

namespace EventBusInbox.TypeConverters.Profiles
{
    internal class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<EventBusQueueModel, EventBusQueue>()
                .ConvertUsing<EventBusQueueTypeConverter>();

            CreateMap<EventBusReceivedMessageModel, EventBusReceivedMessage>()
                .ConvertUsing<EventBusReceivedMessageTypeConverter>();

            CreateMap<IList<ProcessingHistoryLineModel>, IList<ProcessingHistoryLine>>()
                .ConvertUsing<ProcessingHistoryLineTypeConverter>();
        }
    }
}
