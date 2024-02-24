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
        private readonly EventBusInboxDbContext context;
        private readonly IMapper mapper;

        public EventBusQueueRepository(EnvironmentSettings envSettings, IMapper mapper)
        {
            context = new EventBusInboxDbContext(envSettings);
            this.mapper = mapper;
        }

        public async Task<AppResponse<object>> Delete(Guid id)
        {
            var filter = Builders<EventBusQueueModel>.Filter.Eq(x => x.Id, id);
            var result = await context.Queues.DeleteOneAsync(filter);
            if (result.DeletedCount == 0)
                return AppResponse<object>.Custom(HttpStatusCode.InternalServerError, "An error occurred when deleting queue!");

            return AppResponse<object>.Success("Queue deleted!");
        }

        public async Task<EventBusQueue> GetById(Guid id) =>
            await GetByFilter(Builders<EventBusQueueModel>.Filter.Eq(x => x.Id, id));

        public async Task<EventBusQueue> GetByName(string name) =>
            await GetByFilter(Builders<EventBusQueueModel>.Filter.Eq(x => x.Name, name));

        public async Task<AppResponse<object>> Save(EventBusQueue obj)
        {
            var model = mapper.Map<EventBusQueueModel>(obj);

            var filter = new FilterDefinitionBuilder<EventBusQueueModel>().Eq(x => x.Id, obj.Id);
            var result = await context.Queues.ReplaceOneAsync(filter, model, new ReplaceOptions { IsUpsert = true });
            if (result.ModifiedCount == 0)
                return AppResponse<object>.Custom(HttpStatusCode.InternalServerError, "An error occurred when save queue!");

            return AppResponse<object>.Success("Queue saved!");
        }

        private async Task<EventBusQueue> GetByFilter(FilterDefinition<EventBusQueueModel> filter)
        {
            var result = await context.Queues.FindAsync(filter);
            if (!result.Any())
                return null;

            return mapper.Map<EventBusQueue>(result.First());
        }
    }
}
