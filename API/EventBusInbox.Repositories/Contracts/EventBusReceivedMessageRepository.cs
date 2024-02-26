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
        private readonly EnvironmentSettings envSettings;
        private readonly IMapper mapper;

        public EventBusReceivedMessageRepository(EnvironmentSettings envSettings, IMapper mapper)
        {
            this.envSettings = envSettings;
            this.mapper = mapper;
        }

        public async Task<AppResponse<object>> Delete(Guid id)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                var filter = Builders<EventBusReceivedMessageModel>.Filter.Eq(x => x.RequestId, id);
                var result = await context.ReceivedMessages.DeleteOneAsync(filter);
                if (result.DeletedCount == 0)
                    return AppResponse<object>.Custom(HttpStatusCode.InternalServerError, "An error occurred when deleting message!");

                return AppResponse<object>.Success("Message deleted!");
            }
        }

        public async Task<EventBusReceivedMessage> GetById(Guid id)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                return await GetByFilter(context, Builders<EventBusReceivedMessageModel>.Filter.Eq(x => x.RequestId, id));
            }
        }

        public async Task<AppResponse<object>> Save(EventBusReceivedMessage obj)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                var model = mapper.Map<EventBusReceivedMessageModel>(obj);

                var filter = new FilterDefinitionBuilder<EventBusReceivedMessageModel>().Eq(x => x.RequestId, obj.RequestId);
                var result = await context.ReceivedMessages.ReplaceOneAsync(filter, model, new ReplaceOptions { IsUpsert = true });
                
                return AppResponse<object>.Success("Message saved!");
            }
        }

        private async Task<EventBusReceivedMessage> GetByFilter(EventBusInboxDbContext context, 
            FilterDefinition<EventBusReceivedMessageModel> filter)
        {
            var model = await context.ReceivedMessages.Find(filter).FirstOrDefaultAsync();
            if (model is null)
                return null;

            return mapper.Map<EventBusReceivedMessage>(model);
        }
    }
}
