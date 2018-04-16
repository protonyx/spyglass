using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Spyglass.SDK.Data;
using Spyglass.SDK.Services;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using Spyglass.Data.MongoDb.Repository;

namespace Spyglass.Data.MongoDb
{
    public class SpyglassMongoContext : IDataContext
    {
        static SpyglassMongoContext()
        {
            var providers = ProviderService.GetProviders();
            foreach (var provider in providers)
            {
                var map = new BsonClassMap(provider.Value);
                map.AutoMap();
                map.SetDiscriminator(provider.Value.Name);

                if (!BsonClassMap.IsClassMapRegistered(provider.Value))
                {
                    BsonClassMap.RegisterClassMap(map);
                }
            }
        }

        protected ILogger Logger { get; set; }
      
        protected MongoClient Client { get; }

        protected IMongoDatabase Database { get; set; }

        public SpyglassMongoContext(
          IConfigurationRoot configuration,
          ILogger<SpyglassMongoContext> logger)
        {
          this.Logger = logger;
          
            var cs = configuration.GetConnectionString("MongoDb");
            this.Logger.LogInformation(cs);

            this.Client = new MongoClient(cs);
            //this.Client.Settings.ConnectTimeout = TimeSpan.FromSeconds(10);
            this.Database = this.Client.GetDatabase("spyglass");
        }

        public IRepository<TModel> Repository<TModel>()
            where TModel : IHasKey
        {
            var collection = this.Database.GetCollection<TModel>(typeof(TModel).Name);
            return new MongoRepository<TModel>(collection);
        }
    }
}
