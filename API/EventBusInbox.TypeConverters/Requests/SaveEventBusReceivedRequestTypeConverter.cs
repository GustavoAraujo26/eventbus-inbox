using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Requests.EventBusReceivedMessage;

namespace EventBusInbox.TypeConverters.Requests
{
    internal class SaveEventBusReceivedRequestTypeConverter : ITypeConverter<SaveEventBusReceivedMessageRequest, EventBusReceivedMessage>
    {
        public EventBusReceivedMessage Convert(SaveEventBusReceivedMessageRequest source, EventBusReceivedMessage destination, ResolutionContext context) =>
            EventBusReceivedMessage.Create(source.RequestId, source.CreatedAt, source.Type, System.Convert.ToString(source.Content));
    }
}
