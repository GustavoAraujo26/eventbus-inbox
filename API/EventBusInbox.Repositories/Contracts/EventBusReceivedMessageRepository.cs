using AutoMapper;
using EventBusInbox.Domain.Entities;
using EventBusInbox.Domain.Enums;
using EventBusInbox.Domain.Models;
using EventBusInbox.Domain.Repositories;
using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Repositories.DbContext;
using EventBusInbox.Shared.Extensions;
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
                var model = await GetByFilter(context, Builders<EventBusReceivedMessageModel>.Filter.Eq(x => x.RequestId, id));
                if (model is null)
                    return null;

                return mapper.Map<EventBusReceivedMessage>(model);
            }
        }

        public async Task<GetEventBusReceivedMessageResponse> GetResponse(Guid id)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                var model = await GetByFilter(context, Builders<EventBusReceivedMessageModel>.Filter.Eq(x => x.RequestId, id));
                if (model is null)
                    return null;

                return mapper.Map<GetEventBusReceivedMessageResponse>(model);
            }
        }

        public async Task<List<GetEventBusReceivedMessageListResponse>> List(GetEventBusReceivedMessageListRequest request)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                var filter = BuildFilter(request);
                var modelList = await ListPagedByFilter(context, filter, request.Page, request.PageSize);
                if (modelList is null || !modelList.Any())
                    return new List<GetEventBusReceivedMessageListResponse>();

                return mapper.Map<List<GetEventBusReceivedMessageListResponse>>(modelList);
            }
        }

        public async Task<List<GetEventBusReceivedMessageToProcessResponse>> List(GetEventBusReceivedMessageToProcessRequest request)
        {
            using (var context = new EventBusInboxDbContext(envSettings))
            {
                var filter = BuildFilter(request);
                var modelList = await ListPagedByFilter(context, filter, request.Page, request.PageSize);
                if (modelList is null || !modelList.Any())
                    return new List<GetEventBusReceivedMessageToProcessResponse>();

                return mapper.Map<List<GetEventBusReceivedMessageToProcessResponse>>(modelList);
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

        public async Task<List<KeyValuePair<Guid, SummarizeEventBusReceivedMessagesResponse>>> Summarize(List<Guid> queueIdList)
        {
            var statusList = EventBusMessageStatus.Pending.List<EventBusMessageStatus>();
            var filterBuilder = new FilterDefinitionBuilder<EventBusReceivedMessageModel>();

            using (var context = new EventBusInboxDbContext(envSettings))
            {
                var result = new List<KeyValuePair<Guid, SummarizeEventBusReceivedMessagesResponse>>();

                if (queueIdList is null || !queueIdList.Any())
                    return result;

                foreach(var id in queueIdList)
                {
                    foreach(var status in statusList)
                    {
                        var filter = filterBuilder.Where(x => ((int)x.Status).Equals(status.IntKey) && x.Queue.Id.Equals(id));
                        var count = await CountByFilter(context, filter);
                        var summarization = new SummarizeEventBusReceivedMessagesResponse(status, count);
                        result.Add(new KeyValuePair<Guid, SummarizeEventBusReceivedMessagesResponse>(id, summarization));
                    }
                }

                return result;
            }
        }

        private async Task<EventBusReceivedMessageModel> GetByFilter(EventBusInboxDbContext context,
            FilterDefinition<EventBusReceivedMessageModel> filter) =>
            await context.ReceivedMessages.Find(filter).FirstOrDefaultAsync();

        private async Task<long> CountByFilter(EventBusInboxDbContext context,
            FilterDefinition<EventBusReceivedMessageModel> filter) =>
            await context.ReceivedMessages.Find(filter).CountDocumentsAsync();

        private async Task<List<EventBusReceivedMessageModel>> ListPagedByFilter(EventBusInboxDbContext context,
            FilterDefinition<EventBusReceivedMessageModel> filter, int page, int pageSize)
        {
            int skip = (page == 1) ? 0 : (page - 1) * pageSize;

            return await context.ReceivedMessages.Find(filter).Skip(skip).Limit(pageSize).ToListAsync();
        }

        private FilterDefinition<EventBusReceivedMessageModel> BuildFilter(GetEventBusReceivedMessageListRequest request)
        {
            var filterBuilder = new FilterDefinitionBuilder<EventBusReceivedMessageModel>();
            FilterDefinition<EventBusReceivedMessageModel> filter = null;

            if (request.QueueId.HasValue)
                filter = filterBuilder.Eq(x => x.Queue.Id, request.QueueId.Value);

            if (request.CreationDateSearch is not null)
            {
                var creationStartDateFilter = filterBuilder.Gte(x => x.CreatedAt, request.CreationDateSearch.Start.Value);
                var creationEndDateFilter = filterBuilder.Lte(x => x.CreatedAt, request.CreationDateSearch.End.Value);

                filter = filter & creationStartDateFilter & creationEndDateFilter;
            }

            if (request.UpdateDateSearch is not null)
            {
                var updateStartDateFilter = filterBuilder.Gte(x => x.CreatedAt, request.CreationDateSearch.Start.Value);
                var updateEndDateFilter = filterBuilder.Lte(x => x.CreatedAt, request.CreationDateSearch.End.Value);

                filter = filter & updateStartDateFilter & updateEndDateFilter;
            }

            if (request.StatusToSearch is not null && request.StatusToSearch.Any())
            {
                var statusFilter = filterBuilder.Where(x => request.StatusToSearch.Contains(x.Status));

                filter = filter & statusFilter;
            }

            if (!string.IsNullOrEmpty(request.TypeMatch))
            {
                var typeFilter = filterBuilder.Where(x => x.Type.Contains(request.TypeMatch));

                filter = filter & filter;
            }

            if (filter is null)
                filter = filterBuilder.Empty;

            return filter;
        }

        private FilterDefinition<EventBusReceivedMessageModel> BuildFilter(GetEventBusReceivedMessageToProcessRequest request)
        {
            var filterBuilder = new FilterDefinitionBuilder<EventBusReceivedMessageModel>();

            var statusEnabled = new List<EventBusMessageStatus>
            {
                EventBusMessageStatus.Pending, EventBusMessageStatus.TemporaryFailure
            };

            return filterBuilder.Eq(x => x.Queue.Id, request.QueueId) & 
                filterBuilder.Where(x => statusEnabled.Contains(x.Status));
        }
    }
}
