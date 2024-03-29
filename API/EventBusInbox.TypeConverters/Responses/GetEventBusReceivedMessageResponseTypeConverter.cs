﻿using AutoMapper;
using EventBusInbox.Domain.Models;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Extensions;
using Newtonsoft.Json;

namespace EventBusInbox.TypeConverters.Responses
{
    internal class GetEventBusReceivedMessageResponseTypeConverter : 
        ITypeConverter<EventBusReceivedMessageModel, GetEventBusReceivedMessageResponse>
    {
        public GetEventBusReceivedMessageResponse Convert(EventBusReceivedMessageModel source,
            GetEventBusReceivedMessageResponse destination, ResolutionContext context) =>
            new GetEventBusReceivedMessageResponse
            {
                RequestId = source.RequestId,
                CreatedAt = source.CreatedAt,
                Type = source.Type,
                Content = JsonConvert.DeserializeObject<dynamic>(source.Content),
                Queue = context.Mapper.Map<GetEventBusQueueResponse>(source.Queue),
                Status = source.Status.GetData(),
                ProcessingAttempts = source.ProcessingAttempts,
                ProcessingHistory = context.Mapper.Map<List<ProcessingHistoryLineResponse>>(source.ProcessingHistory)
            };
    }
}
