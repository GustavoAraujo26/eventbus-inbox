using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Models;

namespace EventBusInbox.TypeConverters.Entities
{
    internal class EventBusQueueListTypeConverter : ITypeConverter<List<EventBusQueueModel>, List<EventBusQueue>>
    {
        public List<EventBusQueue> Convert(List<EventBusQueueModel> source, List<EventBusQueue> destination, 
            ResolutionContext context)
        {
            var result = new List<EventBusQueue>();

            if (source is null || !source.Any())
                return result;

            source.ForEach(x => result.Add(context.Mapper.Map<EventBusQueue>(x)));

            return result;
        }
    }
}
