using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Tsukaba.Models.DatabaseModels
{
    public class Post
    {
        [BsonId]
        public int Id { get; set; }

        [StringLength(100)]
        public string Topic { get; set; }

        [Required]
        [StringLength(15000)]
        public string Message { get; set; }

        [StringLength(250)]
        public string ImageUrl { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public int TopicId { get; set; }

    }
}