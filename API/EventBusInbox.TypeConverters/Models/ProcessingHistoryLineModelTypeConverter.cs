using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Models;

namespace EventBusInbox.TypeConverters.Models
{
    internal class ProcessingHistoryLineModelTypeConverter : ITypeConverter<IList<ProcessingHistoryLine>, IList<ProcessingHistoryLineModel>>
    {
        public IList<ProcessingHistoryLineModel> Convert(IList<ProcessingHistoryLine> source, IList<ProcessingHistoryLineModel> destination, ResolutionContext context)
        {
            var result = new List<ProcessingHistoryLineModel>();

            if (source is null || !source.Any())
                return result;

            foreach (var item in source)
                result.Add(Convert(item));

            return result;
        }

        private ProcessingHistoryLineModel Convert(ProcessingHistoryLine source) =>
            new ProcessingHistoryLineModel
            {
                OccurredAt = source.OccurredAt,
                ResultMessage = source.ResultMessage,
                 ResultStatus = source.ResultStatus
            };
    }
}
