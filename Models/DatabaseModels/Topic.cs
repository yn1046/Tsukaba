using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;

namespace Tsukaba.Models.DatabaseModels
{
    public class Topic
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
        public int BoardId { get; set; }

        [ForeignKey(nameof(BoardId))]
        public virtual Board MyBoard { get; set; }
    }
}