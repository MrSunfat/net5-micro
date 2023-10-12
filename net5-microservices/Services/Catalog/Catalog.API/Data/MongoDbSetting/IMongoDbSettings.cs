using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Data.MongoDb
{
    public interface IMongoDbSettings
    {
        string CollectionName { get; set; }
        string ConnectionStrings { get; set; }
        string DatabaseName { get; set; }
    }
}
