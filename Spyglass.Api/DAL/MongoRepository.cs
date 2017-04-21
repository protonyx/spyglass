using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Spyglass.Api.DAL
{
    public class MongoRepository<T> where T : class
    {
        protected MongoUnitOfWork UnitOfWork { get; }
        protected IMongoCollection<T> InternalCollection { get; }

        public MongoRepository(MongoUnitOfWork uow, IMongoCollection<T> collection)
        {
            this.UnitOfWork = uow;
            this.InternalCollection = collection;
        }

        public void Add(T entity)
        {
            this.InternalCollection.InsertOne(entity);
        }

        public void Update(object key, T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", key);
            //var update = Builders<T>.Update.Set(".", entity);
            this.InternalCollection.ReplaceOne(filter, entity);
        }

        public void Remove(object key)
        {
            var filter = Builders<T>.Filter.Eq("_id", key);
            this.InternalCollection.DeleteOne(filter);
        }

        public IEnumerable<T> GetAll()
        {
            return this.InternalCollection.Find(new BsonDocument()).ToList();
        }

        public T Get(object key)
        {
            var filter = Builders<T>.Filter.Eq("_id", key);
            return this.InternalCollection.Find(filter).FirstOrDefault();
        }
    }
}