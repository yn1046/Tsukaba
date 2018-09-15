using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Tsukaba.Models.DatabaseModels;
using Tsukaba.Models.RepositoryModels;
using Tsukaba.Services.DalServices;

namespace Tsukaba.Models.MongoRepository
{
    public class PostRepository : IRepository<Post>
    {
        private readonly IMongoCollection<Post> context;

        public PostRepository(IOptions<Settings> settings)
        {
            context = new MongoContext(settings)
                .Database
                .GetCollection<Post>("Posts");
        }

        public async Task<IEnumerable<Post>> GetAll() =>
            await context.Find(_ => true).ToListAsync();

        public async Task Create(Post item) =>
            await context.InsertOneAsync(item);

        public async Task Delete(int id) =>
            await context.DeleteOneAsync(
                Builders<Post>.Filter.Eq(p => p.Id, id));

        public async Task<Post> Get(int id) =>
            await context.Find(
                Builders<Post>.Filter.Eq(p => p.Id, id))
                .FirstOrDefaultAsync();

        public async Task Update(Post item) =>
            await context.ReplaceOneAsync(
                Builders<Post>.Filter.Eq(p => p.Id, item.Id),
                item);

        public Task Save() => null;

        private bool disposed = false;
 
        public virtual void Dispose(bool disposing)
        {
            this.disposed = true;
        }
    
        public void Dispose()
        {
            Dispose(true);
        }

    }
}