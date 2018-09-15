using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Tsukaba.Services.DalServices;

namespace Tsukaba.Models.MongoRepository
{
    public class MongoContext
    {
        public IMongoDatabase Database { get; }

        public MongoContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                Database = client.GetDatabase(settings.Value.Database);
        }
    }
}