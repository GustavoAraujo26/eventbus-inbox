using AutoMapper;
using EventBusInbox.Domain.Models;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Extensions;

namespace EventBusInbox.TypeConverters.Responses
{
    internal class GetEventBusReceivedMessageListResponseTypeConverter :
        ITypeConverter<List<EventBusReceivedMessageModel>, List<GetEventBusReceivedMessageListResponse>>
    {
        public List<GetEventBusReceivedMessageListResponse> Convert(List<EventBusReceivedMessageModel> source, 
            List<GetEventBusReceivedMessageListResponse> destination, ResolutionContext context)
        {
            var result = new List<GetEventBusReceivedMessageListResponse>();

            if (source is null || !source.Any())
                return result;

            source.ForEach(x => result.Add(new GetEventBusReceivedMessageListResponse
            {
                RequestId = x.RequestId,
                CreatedAt = x.CreatedAt,
                Type = x.Type,
                Queue = context.Mapper.Map<GetEventBusQueueResponse>(x.Queue),
                Status = x.Status.GetData(),
                ProcessingAttempts = x.ProcessingAttempts,
                LastUpdate = x.ProcessingHistory is null || !x.ProcessingHistory.Any() ? null : 
                    context.Mapper.Map<ProcessingHistoryLineResponse>(x.ProcessingHistory.OrderByDescending(x => x.OccurredAt).FirstOrDefault())
            }));

            return result;
        }
    }
}
