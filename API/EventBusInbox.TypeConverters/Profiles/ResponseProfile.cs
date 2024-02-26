using AutoMapper;
using EventBusInbox.Domain.Models;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.TypeConverters.Responses;

namespace EventBusInbox.TypeConverters.Profiles
{
    internal class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap<EventBusQueueModel, GetEventBusQueueResponse>()
                .ConvertUsing<GetEventBusQueueResponseTypeConverter>();

            CreateMap<List<EventBusQueueModel>, List<GetEventBusQueueResponse>>()
                .ConvertUsing<GetEventBusQueueResponseListTypeConverter>();
        }
    }
}
