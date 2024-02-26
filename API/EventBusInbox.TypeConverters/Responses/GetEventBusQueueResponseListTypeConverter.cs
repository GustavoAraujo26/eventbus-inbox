using AutoMapper;
using EventBusInbox.Domain.Models;
using EventBusInbox.Domain.Responses.EventBusQueues;

namespace EventBusInbox.TypeConverters.Responses
{
    internal class GetEventBusQueueResponseListTypeConverter : ITypeConverter<List<EventBusQueueModel>, List<GetEventBusQueueResponse>>
    {
        public List<GetEventBusQueueResponse> Convert(List<EventBusQueueModel> source, List<GetEventBusQueueResponse> destination, ResolutionContext context)
        {
            var result = new List<GetEventBusQueueResponse>();

            if (source is null || !source.Any())
                return result;

            source.ForEach(x => result.Add(context.Mapper.Map<GetEventBusQueueResponse>(x)));

            return result;
        }
    }
}
