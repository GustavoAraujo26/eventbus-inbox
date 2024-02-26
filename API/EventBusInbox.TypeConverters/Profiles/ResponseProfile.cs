using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.TypeConverters.Responses;

namespace EventBusInbox.TypeConverters.Profiles
{
    internal class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap<EventBusQueue, GetEventBusQueueResponse>()
                .ConvertUsing<GetEventBusQueueResponseTypeConverter>();
        }
    }
}
