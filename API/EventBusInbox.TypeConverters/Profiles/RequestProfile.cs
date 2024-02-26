﻿using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Requests.EventBusQueues;
using EventBusInbox.TypeConverters.Requests;

namespace EventBusInbox.TypeConverters.Profiles
{
    internal class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<SaveEventBusQueueRequest, EventBusQueue>()
                .ConvertUsing<SaveEventBusQueueRequestTypeConverter>();
        }
    }
}
