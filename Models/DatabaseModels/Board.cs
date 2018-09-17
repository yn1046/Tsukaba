using System.ComponentModel.DataAnnotations;

namespace Tsukaba.Models.DatabaseModels
{
    public class Board
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string FullTitle { get; set; }

        [StringLength(15000)]
        public string Description { get; set; }
    }
}