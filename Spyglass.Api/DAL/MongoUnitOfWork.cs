using MongoDB.Driver;
using MongoDB.Bson;

namespace Spyglass.Api.DAL
{
    public class MongoUnitOfWork
    {
        protected IMongoDatabase InternalDatabase { get; }

        public MongoUnitOfWork(IMongoDatabase db)
        {
            this.InternalDatabase = db;
        }

        public MongoRepository<T> Repository<T>() where T : class
        {
            var collection = this.InternalDatabase.GetCollection<T>(typeof(T).Name);

            return new MongoRepository<T>(this, collection);
        }
    }
}