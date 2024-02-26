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
    internal class EventBusQueueRepository : IEventBusQueueRepository
    {
        private readonly EnvironmentSettings envSettings;
        private readonly IMapper mapper;

        public EventBusQueueRepository(EnvironmentSettings envSettings, IMapper mapper)
        {
            this.envSettings = envSettings;
            this.mapper = mapper;
        }

        public async Task<AppResponse<object>> Delete(Guid id)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                var filter = Builders<EventBusQueueModel>.Filter.Eq(x => x.Id, id);
                var result = await context.Queues.DeleteOneAsync(filter);
                if (result.DeletedCount == 0)
                    return AppResponse<object>.Custom(HttpStatusCode.InternalServerError, "An error occurred when deleting queue!");

                return AppResponse<object>.Success("Queue deleted!");
            }
        }

        public async Task<EventBusQueue> GetById(Guid id)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                return await GetByFilter(context, Builders<EventBusQueueModel>.Filter.Eq(x => x.Id, id));
            }
        }

        public async Task<EventBusQueue> GetByName(string name)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                return await GetByFilter(context, Builders<EventBusQueueModel>.Filter.Eq(x => x.Name, name));
            }
        }

        public async Task<AppResponse<object>> Save(EventBusQueue obj)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                var model = mapper.Map<EventBusQueueModel>(obj);

                var filter = new FilterDefinitionBuilder<EventBusQueueModel>().Eq(x => x.Id, obj.Id);
                var result = await context.Queues.ReplaceOneAsync(filter, model, new ReplaceOptions { IsUpsert = true });
                
                return AppResponse<object>.Success("Queue saved!");
            }
        }

        private async Task<EventBusQueue> GetByFilter(EventBusInboxDbContext context, 
            FilterDefinition<EventBusQueueModel> filter)
        {
            var model = await context.Queues.Find(filter).FirstOrDefaultAsync();
            if (model is null)
                return null;

            return mapper.Map<EventBusQueue>(model);
        }
    }
}
