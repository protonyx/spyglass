using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Spyglass.Server.DAL
{
    public class MongoRepositoryFactory : IRepositoryFactory
    {
        protected MongoClient Client { get; }

        protected IMongoDatabase Database { get; set; }

        public MongoRepositoryFactory()
        {
            this.Client = new MongoClient("mongodb://localhost:27017");
            this.Database = this.Client.GetDatabase("spyglass");
        }

        public IRepository<TModel> Create<TModel>()
            where TModel : IHasKey
        {
            var collection = this.Database.GetCollection<TModel>(typeof(TModel).Name);
            return new MongoRepository<TModel>(collection);
        }
    }
}
