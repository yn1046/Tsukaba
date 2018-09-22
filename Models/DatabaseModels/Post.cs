using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tsukaba.Models.DatabaseModels
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NumberOnBoard { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(15000)]
        public string Message { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public DateTime LastTimeBumped { get; set; }

        [Required]
        public bool IsSage { get; set; } = false;

        [Required]
        public bool IsOp { get; set; } = false;

        [Required]
        public bool IsPinned { get; set; } = false;

        [Required]
        public bool IsLocked { get; set; } = false;

        [Required]
        public int ParentId { get; set; } = 0;

        [Required]
        public int BoardId { get; set; }

        [ForeignKey(nameof(BoardId))]
        public virtual Board MyBoard { get; set; }

    }
}