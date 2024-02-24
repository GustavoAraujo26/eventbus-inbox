using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Models;

namespace EventBusInbox.TypeConverters.Entities
{
    internal class ProcessingHistoryLineTypeConverter : ITypeConverter<IList<ProcessingHistoryLineModel>, IList<ProcessingHistoryLine>>
    {
        public IList<ProcessingHistoryLine> Convert(IList<ProcessingHistoryLineModel> source, IList<ProcessingHistoryLine> destination, ResolutionContext context)
        {
            var result = new List<ProcessingHistoryLine>();

            if (source is null || !source.Any())
                return result;

            foreach(var item in source )
                result.Add(Convert(item));

            return result;
        }

        public ProcessingHistoryLine Convert(ProcessingHistoryLineModel source) =>
            ProcessingHistoryLine.Create(source.OccurredAt, source.ResultStatus, source.ResultMessage);
    }
}
