using AutoMapper;
using EventBusInbox.Domain.Models;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;

namespace EventBusInbox.TypeConverters.Responses
{
    internal class GetEventBusReceivedMessageToProcessResponseTypeConverter :
        ITypeConverter<List<EventBusReceivedMessageModel>, List<GetEventBusReceivedMessageToProcessResponse>>
    {
        public List<GetEventBusReceivedMessageToProcessResponse> Convert(List<EventBusReceivedMessageModel> source, 
            List<GetEventBusReceivedMessageToProcessResponse> destination, ResolutionContext context)
        {
            var result = new List<GetEventBusReceivedMessageToProcessResponse>();

            if (source is null || !source.Any())
                return result;

            source.ForEach(x => result.Add(Convert(x, context)));

            return result;
        }

        private GetEventBusReceivedMessageToProcessResponse Convert(EventBusReceivedMessageModel source, ResolutionContext context) =>
            new GetEventBusReceivedMessageToProcessResponse
            {
                RequestId = source.RequestId,
                CreatedAt = source.CreatedAt,
                Type = source.Type,
                Data = source.Data,
                Queue = context.Mapper.Map<GetEventBusQueueResponse>(source.Queue)
            };
    }
}
