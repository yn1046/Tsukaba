using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tsukaba.Models.DatabaseModels
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(15000)]
        public string Message { get; set; }

        [StringLength(250)]
        public string ImageUrl { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public int TopicId { get; set; }

        [ForeignKey(nameof(TopicId))]
        public virtual Topic MyTopic { get; set; }

    }
}