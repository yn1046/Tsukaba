using System;
using Microsoft.Extensions.Options;
using Tsukaba.Models.MongoRepository;
using Tsukaba.Services.DalServices;

namespace Tsukaba.Models
{
    public class MongoUnitOfWork
    {
        private readonly MongoContext context;
        private PostRepository postRepository;
        private TopicRepository topicRepository;

        public MongoUnitOfWork(IOptions<Settings> settings)
        {
            context = new MongoContext(settings);
        }

        public PostRepository Posts {
            get {
                if (postRepository == null)
                    postRepository = new PostRepository(context);
                return postRepository;
            }
        }

        public TopicRepository Topics {
            get {
                if (topicRepository == null)
                    topicRepository = new TopicRepository(context);
                return topicRepository;
            }
        }
    }
}