using MongoDB.Driver;

namespace Spyglass.Api.DAL
{
    public class MongoUnitOfWorkFactory
    {
        public static MongoUnitOfWorkFactory Instance = new MongoUnitOfWorkFactory();

        protected MongoClient Client { get; }

        public MongoUnitOfWorkFactory()
        {
            this.Client = new MongoClient("mongodb://localhost:27017");
        }

        public MongoUnitOfWork Create()
        {
            var db = this.Client.GetDatabase("spyglass");

            return new MongoUnitOfWork(db);
        }
    }
}