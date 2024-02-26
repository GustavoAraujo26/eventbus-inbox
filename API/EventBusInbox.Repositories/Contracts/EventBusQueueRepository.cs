using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Models;
using EventBusInbox.Domain.Repositories;
using EventBusInbox.Domain.Requests.EventBusQueues;
using EventBusInbox.Domain.Responses.EventBusQueues;
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

        public async Task<GetEventBusQueueResponse> Get(GetEventBusQueueRequest request)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                FilterDefinition<EventBusQueueModel> filter = null;
                if (request.Id.HasValue)
                    filter = Builders<EventBusQueueModel>.Filter.Eq(x => x.Id, request.Id.Value);
                else if (!string.IsNullOrEmpty(request.Name))
                    filter = Builders<EventBusQueueModel>.Filter.Eq(x => x.Name, request.Name);
                else
                    return null;

                var model = await GetByFilter(context, filter);
                if (model is null)
                    return null;

                return mapper.Map<GetEventBusQueueResponse>(model);
            }
        }

        public async Task<EventBusQueue> GetById(Guid id)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                var model = await GetByFilter(context, Builders<EventBusQueueModel>.Filter.Eq(x => x.Id, id));
                if (model is null)
                    return null;

                return mapper.Map<EventBusQueue>(model);
            }
        }

        public async Task<EventBusQueue> GetByName(string name)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                var model = await GetByFilter(context, Builders<EventBusQueueModel>.Filter.Eq(x => x.Name, name));
                if (model is null)
                    return null;

                return mapper.Map<EventBusQueue>(model);
            }
        }

        public async Task<List<GetEventBusQueueResponse>> List(GetEventBusQueueListRequest request)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                int skip = request.Page.Equals(1) ? 0 : (request.Page - 1) * request.PageSize;
                
                FilterDefinition<EventBusQueueModel> filter = null;

                if (!string.IsNullOrEmpty(request.NameMatch))
                {
                    filter = new FilterDefinitionBuilder<EventBusQueueModel>()
                        .Where(x => x.Name.ToLowerInvariant().Contains(request.NameMatch.ToLowerInvariant()));
                }
                else if (!string.IsNullOrEmpty(request.DescriptionMatch))
                {
                    filter = new FilterDefinitionBuilder<EventBusQueueModel>()
                        .Where(x => x.Description.ToLowerInvariant().Contains(request.DescriptionMatch.ToLowerInvariant()));
                }
                else if (request.Status.HasValue)
                {
                    filter = new FilterDefinitionBuilder<EventBusQueueModel>().Eq(x => x.Status, request.Status);
                }
                else
                {
                    filter = new FilterDefinitionBuilder<EventBusQueueModel>().Empty;
                }

                var modelList = await context.Queues.Find(filter).Skip(skip).Limit(request.PageSize).ToListAsync();
                if (modelList is null || !modelList.Any())
                    return new List<GetEventBusQueueResponse>();

                return mapper.Map<List<GetEventBusQueueResponse>>(modelList);
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

        private async Task<EventBusQueueModel> GetByFilter(EventBusInboxDbContext context, 
            FilterDefinition<EventBusQueueModel> filter) =>
            await context.Queues.Find(filter).FirstOrDefaultAsync();

        private async Task<List<EventBusQueueModel>> ListByFilter(EventBusInboxDbContext context, 
            FilterDefinition<EventBusQueueModel> filter) =>
            await context.Queues.Find(filter).ToListAsync();
    }
}
