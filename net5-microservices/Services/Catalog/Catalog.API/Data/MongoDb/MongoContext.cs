using Catalog.API.Data.MongoDb;
using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data.MongoDb
{
    public class MongoContext : IMongoContext
    {
        public IMongoDatabase Database { get; }

        public MongoContext(IMongoDbSettings mongoDbSettings)
        {

            var client = new MongoClient(mongoDbSettings.ConnectionStrings);
            Database = client.GetDatabase(mongoDbSettings.DatabaseName);
            //CatalogContextSeed.SeedData(Products);
        }
    }
}
