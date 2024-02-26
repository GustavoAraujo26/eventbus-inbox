using AutoMapper;
using EventBusInbox.Domain.Models;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;

namespace EventBusInbox.TypeConverters.Responses
{
    internal class ProcessingHistoryLineResponseListTypeConverter : 
        ITypeConverter<List<ProcessingHistoryLineModel>, List<ProcessingHistoryLineResponse>>
    {
        public List<ProcessingHistoryLineResponse> Convert(List<ProcessingHistoryLineModel> source, 
            List<ProcessingHistoryLineResponse> destination, ResolutionContext context)
        {
            var result = new List<ProcessingHistoryLineResponse>();

            if (source is null || !source.Any())
                return result;

            source.ForEach(x => result.Add(context.Mapper.Map<ProcessingHistoryLineResponse>(x)));

            return result;
        }
    }
}
