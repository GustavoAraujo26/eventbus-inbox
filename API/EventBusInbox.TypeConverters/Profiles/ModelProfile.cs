using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Models;
using EventBusInbox.TypeConverters.Models;

namespace EventBusInbox.TypeConverters.Profiles
{
    internal class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<EventBusQueue, EventBusQueueModel>()
                .ConvertUsing<EventBusQueueModelTypeConverter>();

            CreateMap<EventBusReceivedMessage, EventBusReceivedMessageModel>()
                .ConvertUsing<EventBusReceivedMessageModelTypeConverter>();

            CreateMap<IList<ProcessingHistoryLine>, IList<ProcessingHistoryLineModel>>()
                .ConvertUsing<ProcessingHistoryLineModelTypeConverter>();
        }
    }
}
