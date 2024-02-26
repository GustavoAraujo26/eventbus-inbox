using AutoMapper;
using EventBusInbox.Domain.Models;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
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

            CreateMap<List<EventBusReceivedMessageModel>, List<GetEventBusReceivedMessageListResponse>>()
                .ConvertUsing<GetEventBusReceivedMessageListResponseTypeConverter>();

            CreateMap<EventBusReceivedMessageModel, GetEventBusReceivedMessageResponse>()
                .ConvertUsing<GetEventBusReceivedMessageResponseTypeConverter>();

            CreateMap<List<EventBusReceivedMessageModel>, List<GetEventBusReceivedMessageToProcessResponse>>()
                .ConvertUsing<GetEventBusReceivedMessageToProcessResponseTypeConverter>();

            CreateMap<List<ProcessingHistoryLineModel>, List<ProcessingHistoryLineResponse>>()
                .ConvertUsing<ProcessingHistoryLineResponseListTypeConverter>();

            CreateMap<ProcessingHistoryLineModel, ProcessingHistoryLineResponse>()
                .ConvertUsing<ProcessingHistoryLineResponseTypeConverter>();
        }
    }
}
