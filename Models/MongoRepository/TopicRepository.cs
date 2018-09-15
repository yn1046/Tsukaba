using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Tsukaba.Models.DatabaseModels;
using Tsukaba.Services.DalServices;

namespace Tsukaba.Models.MongoRepository
{
    public class TopicRepository : IRepository<Topic>
    {
        private readonly IMongoCollection<Topic> topics;

        public TopicRepository(MongoContext context)
        {
            topics = context.Database.GetCollection<Topic>("Topics");
        }

        public async Task<IEnumerable<Topic>> GetAll() =>
            await topics.Find(_ => true).ToListAsync();

        public async Task Create(Topic item) =>
            await topics.InsertOneAsync(item);

        public async Task Delete(int id) =>
            await topics.DeleteOneAsync(
                Builders<Topic>.Filter.Eq(p => p.Id, id));

        public async Task<Topic> Get(int id) =>
            await topics.Find(
                Builders<Topic>.Filter.Eq(p => p.Id, id))
                .FirstOrDefaultAsync();

        public async Task Update(Topic item) =>
            await topics.ReplaceOneAsync(
                Builders<Topic>.Filter.Eq(p => p.Id, item.Id),
                item);

    }
}