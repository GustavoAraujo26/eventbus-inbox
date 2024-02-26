using EventBusInbox.Domain.Models;
using EventBusInbox.Repositories.Enums;
using EventBusInbox.Shared.Models;
using MongoDB.Driver;

namespace EventBusInbox.Repositories.DbContext
{
    internal class EventBusInboxDbContext : IDisposable
    {
        private readonly IMongoDatabase database;

        public EventBusInboxDbContext(EnvironmentSettings envSettings) => 
            database = new MongoClient(envSettings.MongoDbCredentials.Value)
            .GetDatabase(envSettings.MongoDbCredentials.Key);

        public IMongoCollection<EventBusQueueModel> Queues
        {
            get => database.GetCollection<EventBusQueueModel>(EventBusInboxCollection.EventBusQueues.ToString());
        }

        public IMongoCollection<EventBusReceivedMessageModel> ReceivedMessages
        {
            get => database.GetCollection<EventBusReceivedMessageModel>(EventBusInboxCollection.EventBusReceivedMessages.ToString());
        }

        public void Dispose() { }
    }
}
