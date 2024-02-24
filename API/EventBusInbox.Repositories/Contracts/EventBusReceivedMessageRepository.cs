using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Models;
using EventBusInbox.Domain.Repositories;
using EventBusInbox.Repositories.DbContext;
using EventBusInbox.Shared.Models;
using MongoDB.Driver;
using System.Net;

namespace EventBusInbox.Repositories.Contracts
{
    internal class EventBusReceivedMessageRepository : IEventBusReceivedMessageRepository
    {
        private readonly EventBusInboxDbContext context;
        private readonly IMapper mapper;

        public EventBusReceivedMessageRepository(EnvironmentSettings envSettings, IMapper mapper)
        {
            context = new EventBusInboxDbContext(envSettings);
            this.mapper = mapper;
        }

        public async Task<AppResponse<object>> Delete(Guid id)
        {
            var filter = Builders<EventBusReceivedMessageModel>.Filter.Eq(x => x.RequestId, id);
            var result = await context.ReceivedMessages.DeleteOneAsync(filter);
            if (result.DeletedCount == 0)
                return AppResponse<object>.Custom(HttpStatusCode.InternalServerError, "An error occurred when deleting message!");

            return AppResponse<object>.Success("Message deleted!");
        }

        public async Task<EventBusReceivedMessage> GetById(Guid id) =>
            await GetByFilter(Builders<EventBusReceivedMessageModel>.Filter.Eq(x => x.RequestId, id));

        public async Task<AppResponse<object>> Save(EventBusReceivedMessage obj)
        {
            var model = mapper.Map<EventBusReceivedMessageModel>(obj);

            var filter = new FilterDefinitionBuilder<EventBusReceivedMessageModel>().Eq(x => x.RequestId, obj.RequestId);
            var result = await context.ReceivedMessages.ReplaceOneAsync(filter, model, new ReplaceOptions { IsUpsert = true });
            if (result.ModifiedCount == 0)
                return AppResponse<object>.Custom(HttpStatusCode.InternalServerError, "An error occurred when save message!");

            return AppResponse<object>.Success("Message saved!");
        }

        private async Task<EventBusReceivedMessage> GetByFilter(FilterDefinition<EventBusReceivedMessageModel> filter)
        {
            var result = await context.ReceivedMessages.FindAsync(filter);
            if (!result.Any())
                return null;

            return mapper.Map<EventBusReceivedMessage>(result.First());
        }
    }
}
