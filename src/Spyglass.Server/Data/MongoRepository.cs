using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Spyglass.Server.Data
{
    public class MongoRepository<TModel> : IRepository<TModel> where TModel : IHasKey
    {
        protected IMongoCollection<TModel> InternalCollection { get; }

        public MongoRepository(IMongoCollection<TModel> collection)
        {
            this.InternalCollection = collection;
        }

        public TModel Get(object key)
        {
            var filter = Builders<TModel>.Filter.Eq("_id", key);
            return this.InternalCollection.Find(filter).FirstOrDefault();
        }

        public IEnumerable<TModel> GetAll()
        {
            return this.InternalCollection
                .Find(new BsonDocument())
                .ToList();
        }

        public IEnumerable<TModel> FindBy(Expression<Func<TModel, bool>> predicate)
        {
            return IAsyncCursorSourceExtensions.ToList(this.InternalCollection
                    .AsQueryable()
                    .Where(predicate));
        }
        public IEnumerable<TModel> FindBy(Func<IQueryable<TModel>, IEnumerable<TModel>> qAction)
        {
            var q = this.InternalCollection.AsQueryable();
            return qAction(q).ToList();
        }

        public TModel Add(TModel entity)
        {
            this.InternalCollection.InsertOne(entity);
            return entity;
        }

        public TModel Update(TModel entity)
        {
            var filter = Builders<TModel>.Filter.Eq("_id", entity.GetKey());
            //var update = Builders<T>.Update.Set(".", entity);
            this.InternalCollection.ReplaceOne(filter, entity);
            return entity;
        }

        public void Delete(TModel entity)
        {
            this.Delete(entity.GetKey());
        }

        public void Delete(object key)
        {
            var filter = Builders<TModel>.Filter.Eq("_id", key);
            this.InternalCollection.DeleteOne(filter);
        }
    }
}
