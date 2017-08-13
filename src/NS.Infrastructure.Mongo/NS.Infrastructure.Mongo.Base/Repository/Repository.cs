using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using NS.Infrastructure.Mongo.Base.Model;

namespace NS.Infrastructure.Mongo.Base.Repository
{
    public abstract class Repository<T> where T : ObjectIdEntity
    {
        protected readonly IMongoCollection<T> MongoCollection;
        protected Repository(string connectionString, string collection)
        {
            var mongoUrl = new MongoUrl(connectionString);
            var client = new MongoClient(mongoUrl);
            var mongoDatabase = client.GetDatabase(mongoUrl.DatabaseName);
            MongoCollection = mongoDatabase.GetCollection<T>(collection);
        }

        public async Task<T> GetById(string id)
        {
            var result = await MongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
            return result;
        }

        public async Task<T> Add(T item)
        {
            await MongoCollection.InsertOneAsync(item).ConfigureAwait(false);
            return item;
        }

        public async Task<bool> DeleteById(string id)
        {
            var result = await MongoCollection.DeleteOneAsync(x => x.Id == id).ConfigureAwait(false);
            return result.IsAcknowledged;
        }
    }
}
