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
        private readonly IMongoCollection<Post> posts;

        public PostRepository(MongoContext context)
        {
            posts = context.Database.GetCollection<Post>("Posts");
        }

        public async Task<IEnumerable<Post>> GetAll() =>
            await posts.Find(_ => true).ToListAsync();

        public async Task Create(Post item) =>
            await posts.InsertOneAsync(item);

        public async Task Delete(int id) =>
            await posts.DeleteOneAsync(
                Builders<Post>.Filter.Eq(p => p.Id, id));

        public async Task<Post> Get(int id) =>
            await posts.Find(
                Builders<Post>.Filter.Eq(p => p.Id, id))
                .FirstOrDefaultAsync();

        public async Task Update(Post item) =>
            await posts.ReplaceOneAsync(
                Builders<Post>.Filter.Eq(p => p.Id, item.Id),
                item);

    }
}