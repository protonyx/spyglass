using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Spyglass.SDK.Data;

namespace Spyglass.Server.DAL
{
    public class MongoRepositoryFactory : IRepositoryFactory
    {
        protected MongoClient Client { get; }

        protected IMongoDatabase Database { get; set; }

        public MongoRepositoryFactory()
        {
            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress("kelvin", 27017),
                ConnectTimeout = TimeSpan.FromSeconds(10)
            };
            this.Client = new MongoClient(settings);
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
