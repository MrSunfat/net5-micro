using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data.MongoDb
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
    }
}
