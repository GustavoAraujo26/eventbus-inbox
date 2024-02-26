using AutoMapper;
using EventBusInbox.Domain.Models;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Extensions;

namespace EventBusInbox.TypeConverters.Responses
{
    internal class ProcessingHistoryLineResponseTypeConverter : ITypeConverter<ProcessingHistoryLineModel, ProcessingHistoryLineResponse>
    {
        public ProcessingHistoryLineResponse Convert(ProcessingHistoryLineModel source, 
            ProcessingHistoryLineResponse destination, ResolutionContext context) =>
            new ProcessingHistoryLineResponse(source.OccurredAt, source.ResultStatus.GetData(), source.ResultMessage);
    }
}
